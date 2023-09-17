import { CreateUserDto } from "../../../service/services/entities/user/create-user.dto";
import { UpdateUserDto } from "../../../service/services/entities/user/update-user.dto";
import { UsersService } from "src/service/services/userService/users.service";
export declare class UsersController {
    private readonly usersService;
    constructor(usersService: UsersService);
    create(createUserDto: CreateUserDto): string;
    findAll(): string;
    findOne(id: string): string;
    update(id: string, updateUserDto: UpdateUserDto): string;
    remove(id: string): string;
}
