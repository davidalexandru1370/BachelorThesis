import {
  ConflictException,
  Injectable,
  NotFoundException,
} from "@nestjs/common";
import { Repository } from "typeorm";
import { InjectRepository } from "@nestjs/typeorm";
import * as bcrypt from "bcrypt";
import { JwtService } from "@nestjs/jwt/dist/jwt.service";
import { ApiErrorCodes } from "../../../core/constants/i18n";
import { AuthResult } from "../../../core/common/authResult.entity";
import { User } from "../../../core/domain/user.entity";

@Injectable()
export class UsersService {
  constructor(
    @InjectRepository(User)
    private userRepository: Repository<User>,
    private jwtService: JwtService,
  ) {}

  async login(email: string, password: string): Promise<AuthResult> {
    const user = await this.userRepository.findOne({
      where: { email: email },
    });

    if (user === null) {
      throw new NotFoundException(
        ApiErrorCodes.EMAIL_OR_PASSWORD_INVALID.toString(),
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
      ApiErrorCodes.EMAIL_OR_PASSWORD_INVALID.toString(),
    );
  }

  async register(email: string, password: string): Promise<AuthResult> {
    const user = await this.userRepository.findOne({
      where: { email: email },
    });

    if (user !== null) {
      throw new ConflictException(
        ApiErrorCodes.EMAIL_ALREADY_EXISTS.toString(),
      );
    }

    password = await bcrypt.hash(password, 10);

    const addedUser = await this.userRepository.save(new User(email, password));
    const token = await this.generateJwtToken(addedUser);

    return new AuthResult(token);
  }

  private async generateJwtToken(user: User): Promise<string> {
    const payload = {
      sub: user.id,
      email: user.email,
    };
    const token = await this.jwtService.signAsync(payload);

    return token;
  }

  async validateUser(email: string, password: string): Promise<User> {
    const user = await this.userRepository.findOne({
      where: { email: email },
    });

    if (user && (await bcrypt.compare(password, user.password))) {
      return user;
    }

    return null;
  }
}
