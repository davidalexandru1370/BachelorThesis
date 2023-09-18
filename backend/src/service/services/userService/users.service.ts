import { Inject, Injectable } from "@nestjs/common";
import { CreateUserRequest } from "../../../presentation/entities/requests/user/create-user.request";
import { UpdateUserDto } from "../../entities/user/update-user.dto";
import { Repository } from "typeorm";
import { User } from "src/core/domain/user.entity";
import { InjectRepository } from "@nestjs/typeorm";

@Injectable()
export class UsersService {
  constructor(
    @InjectRepository(User)
    private userRepository: Repository<User>
  ) {}

  async create(createUserDto: CreateUserRequest): Promise<User> {
    const addedUser = await this.userRepository.save(createUserDto);
    return addedUser;
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
