import { Injectable, NotFoundException } from "@nestjs/common";
import { Repository } from "typeorm";
import { User } from "src/core/domain/user.entity";
import { InjectRepository } from "@nestjs/typeorm";
import * as bcrypt from "bcrypt";
import { JwtService } from "@nestjs/jwt";
import { ApiErrorCodes } from "src/core/constants/i18n";
import { InjectMapper } from "@timonmasberg/automapper-nestjs";

@Injectable()
export class UsersService {
  constructor(
    @InjectRepository(User)
    private userRepository: Repository<User>,
    private jwtService: JwtService
  ) {}

  async login(email: string, password: string): Promise<AuthResult> {
    const user = await this.userRepository.findOne({
      where: { email: email },
    });

    if (user === null) {
      throw new NotFoundException(
        ApiErrorCodes.EMAIL_OR_PASSWORD_INVALID.toString()
      );
    }

    const isMatch = await bcrypt.compare(password, user.password);

    if (isMatch) {
      const payload = {
        sub: user.id,
        email: user.email,
      };
      const token = await this.jwtService.signAsync(payload);
      return new AuthResult(token);
    }

    throw new NotFoundException(
      ApiErrorCodes.EMAIL_OR_PASSWORD_INVALID.toString()
    );
  }

  async register(email: string, password: string): Promise<AuthResult> {
    const user = await this.userRepository.findOne({
      where: { email: email },
    });

    if (user !== null) {
      throw new NotFoundException(
        ApiErrorCodes.EMAIL_ALREADY_EXISTS.toString()
      );
    }

    const addedUser = await this.userRepository.save(new User(email, password));
  }

  private async generateJwtToken(user: User): Promise<string> {
    const payload = {
      sub: user.id,
      email: user.email,
    };
    const token = await this.jwtService.signAsync(payload);

    return token;
  }
}
