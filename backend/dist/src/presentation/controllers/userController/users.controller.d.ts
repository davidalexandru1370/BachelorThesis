import { CreateUserDto } from "../../../service/services/entities/user/create-user.dto";
import { UsersService } from "src/service/services/userService/users.service";
export declare class UsersController {
    private readonly usersService;
    constructor(usersService: UsersService);
    create(createUserDto: CreateUserDto): string;
    findAll(): Promise<import("../../../core/domain/user.entity").User[]>;
}
