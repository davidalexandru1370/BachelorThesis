import { Module } from "@nestjs/common";
import { TypeOrmModule } from "@nestjs/typeorm";
import { configService } from "./presentation/config/config.service";
import { UsersModule } from "./presentation/controllers/userController/users.module";
import { databaseProviders } from "ormconfig";
import { UsersService } from "./service/services/userService/users.service";
import { UsersController } from "./presentation/controllers/userController/users.controller";
import { DataSource } from "typeorm";

@Module({
  imports: [
    TypeOrmModule.forRoot(configService.getTypeOrmConfig()),
    UsersModule,
  ],
  exports: [],
})
export class AppModule {
  constructor(private dataseSource: DataSource) {}
}
