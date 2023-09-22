import { IsString, Matches, MinLength } from "class-validator";

export class LoginUserRequest {
  @IsString()
  @Matches("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]{2,}$")
  email: string;
  @MinLength(5)
  password: string;
}
