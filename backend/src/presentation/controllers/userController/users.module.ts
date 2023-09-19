import { Module } from "@nestjs/common";
import { UsersController } from "./users.controller";
import { UsersService } from "src/service/services/userService/users.service";
import { TypeOrmModule } from "@nestjs/typeorm";
import { User } from "src/core/domain/user.entity";
import { JwtModule } from "@nestjs/jwt";

require("dotenv").config();

@Module({
  imports: [
    TypeOrmModule.forFeature([User]),
    JwtModule.register({
      global: true,
      secret: process.env.JWT_SECRET,
      verifyOptions: {
        ignoreExpiration: false,
        issuer: process.env.JWT_ISSUER,
        algorithms: ["HS256"],
      },
      signOptions: {
        issuer: process.env.JWT_ISSUER,
        algorithm: "HS256",
        expiresIn: parseInt(process.env.JWT_EXPIRE_TIME_IN_MINUTES) * 60,
      },
    }),
  ],
  providers: [UsersService],
  controllers: [UsersController],
})
export class UsersModule {}
