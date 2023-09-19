import { Module } from "@nestjs/common";
import { TypeOrmModule } from "@nestjs/typeorm";
import { configService } from "./presentation/config/config.service";
import { UsersModule } from "./presentation/controllers/userController/users.module";
import { DataSource } from "typeorm";
import { AutomapperModule } from "@timonmasberg/automapper-nestjs";
import { classes } from "@automapper/classes";

@Module({
  imports: [
    AutomapperModule.forRoot({
      strategyInitializer: classes(),
    }),
    TypeOrmModule.forRoot(configService.getTypeOrmConfig()),
    UsersModule,
  ],
  exports: [TypeOrmModule],
})
export class AppModule {
  constructor(private dataseSource: DataSource) {}
}
