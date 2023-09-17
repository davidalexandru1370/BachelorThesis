import { Module } from "@nestjs/common";
import { TypeOrmModule } from "@nestjs/typeorm";
import { configService } from "./presentation/config/config.service";
import { UsersModule } from "./presentation/controllers/userController/users.module";

@Module({
  imports: [
    TypeOrmModule.forRoot(configService.getTypeOrmConfig()),
    UsersModule,
  ],
  controllers: [],
  providers: [],
})
export class AppModule {}
