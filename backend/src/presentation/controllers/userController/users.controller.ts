import { Controller, Get, Post, Body } from "@nestjs/common";
import { CreateUserRequest } from "../../entities/requests/user/create-user.request";
import { UsersService } from "src/service/services/userService/users.service";

@Controller("api/user")
export class UsersController {
  constructor(private readonly usersService: UsersService) {}

  @Post()
  create(@Body() createUserDto: CreateUserRequest) {
    return this.usersService.create(createUserDto);
  }

  @Get()
  findAll() {
    return this.usersService.findAll();
  }
}
