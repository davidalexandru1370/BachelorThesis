import { PartialType } from "@nestjs/mapped-types";
import { CreateUserRequest } from "../../../presentation/entities/requests/user/create-user.request";

export class UpdateUserDto extends PartialType(CreateUserRequest) {}
