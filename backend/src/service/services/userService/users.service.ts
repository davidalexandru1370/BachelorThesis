import { Inject, Injectable } from "@nestjs/common";
import { CreateUserDto } from "../entities/user/create-user.dto";
import { UpdateUserDto } from "../entities/user/update-user.dto";
import { Repository } from "typeorm";
import { User } from "src/core/domain/user.entity";
import { Providers } from "src/core/constants/providers";

@Injectable()
export class UsersService {
  constructor(
    @Inject(Providers.USER_REPOSITORY)
    private userRepository: Repository<User>
  ) {}

  create(createUserDto: CreateUserDto) {
    return "This action adds a new user";
  }

  findAll() {
    return this.userRepository.createQueryBuilder().getMany();
  }

  findOne(id: number) {
    return `This action returns a #${id} user`;
  }

  update(id: number, updateUserDto: UpdateUserDto) {
    return `This action updates a #${id} user`;
  }

  remove(id: number) {
    return `This action removes a #${id} user`;
  }
}
