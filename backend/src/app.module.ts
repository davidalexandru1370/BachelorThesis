import { Module } from "@nestjs/common";
import { TypeOrmModule } from "@nestjs/typeorm";
import { configService } from "./presentation/config/config.service";
import { UsersModule } from "./presentation/controllers/userController/users.module";
import { DataSource } from "typeorm";

@Module({
  imports: [
    TypeOrmModule.forRoot(configService.getTypeOrmConfig()),
    UsersModule,
  ],
  exports: [TypeOrmModule],
})
export class AppModule {
  constructor(private dataseSource: DataSource) {}
}
