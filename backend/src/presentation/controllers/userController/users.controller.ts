import { Controller, Post, Body, HttpCode, HttpStatus } from "@nestjs/common";
import { LoginUserRequest } from "../../entities/requests/user/login-user.request";
import { UsersService } from "src/service/services/userService/users.service";

@Controller("api/user")
export class UsersController {
  constructor(private readonly usersService: UsersService) {}

  @HttpCode(HttpStatus.OK)
  @HttpCode(HttpStatus.NOT_FOUND)
  @Post("login")
  async login(@Body() loginUserRequest: LoginUserRequest): Promise<AuthResult> {
    var { email, password } = loginUserRequest;
    var authResult = await this.usersService.login(email, password);
    return authResult;
  }
}
