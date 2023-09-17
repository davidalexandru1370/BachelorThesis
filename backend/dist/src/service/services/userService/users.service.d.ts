import { CreateUserDto } from "../entities/user/create-user.dto";
import { UpdateUserDto } from "../entities/user/update-user.dto";
export declare class UsersService {
    create(createUserDto: CreateUserDto): string;
    findAll(): string;
    findOne(id: number): string;
    update(id: number, updateUserDto: UpdateUserDto): string;
    remove(id: number): string;
}
