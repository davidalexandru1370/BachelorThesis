import { Module } from "@nestjs/common";
import { TypeOrmModule } from "@nestjs/typeorm";
import { configService } from "./presentation/config/config.service";
import { UsersModule } from "./presentation/controllers/user/users.module";
import { DataSource } from "typeorm";
import { AutomapperModule } from "@timonmasberg/automapper-nestjs";
import { classes } from "@automapper/classes";
import { FolderModule } from "./presentation/controllers/folder/folder.module";

@Module({
  imports: [
    AutomapperModule.forRoot({
      strategyInitializer: classes(),
    }),
    TypeOrmModule.forRoot(configService.getTypeOrmConfig()),
    UsersModule,
    FolderModule,
  ],
  exports: [TypeOrmModule],
})
export class AppModule {
  constructor(private dataseSource: DataSource) {}
}
