import { Module } from "@nestjs/common";
import { PassportModule } from "@nestjs/passport";
import { JwtStrategy } from "./jwt.strategy";
import { UsersService } from "src/service/services/user/users.service";

@Module({
  imports: [PassportModule],
  providers: [JwtStrategy],
})
export class AuthModule {}
