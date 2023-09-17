import { CreateUserDto } from "../entities/user/create-user.dto";
import { UpdateUserDto } from "../entities/user/update-user.dto";
import { Repository } from "typeorm";
import { User } from "src/core/domain/user.entity";
export declare class UsersService {
    private userRepository;
    constructor(userRepository: Repository<User>);
    create(createUserDto: CreateUserDto): string;
    findAll(): Promise<User[]>;
    findOne(id: number): string;
    update(id: number, updateUserDto: UpdateUserDto): string;
    remove(id: number): string;
}
