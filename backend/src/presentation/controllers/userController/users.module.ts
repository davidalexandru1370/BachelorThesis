import { Module } from "@nestjs/common";
import { UsersController } from "./users.controller";
import { UsersService } from "src/service/services/userService/users.service";
import { TypeOrmModule } from "@nestjs/typeorm";
import { configService } from "src/presentation/config/config.service";
import { User } from "src/core/domain/user.entity";

@Module({
  imports: [TypeOrmModule.forFeature([User])],
  providers: [UsersService],
  controllers: [UsersController],
})
export class UsersModule {}
