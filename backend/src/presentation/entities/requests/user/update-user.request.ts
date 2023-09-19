import { PartialType } from "@nestjs/mapped-types";
import { LoginUserRequest } from "./login-user.request";

export class UpdateUserRequest extends PartialType(LoginUserRequest) {}
