import { Module } from "@nestjs/common";
import { AppController } from "./presentation/controllers/app.controller";
import { AppService } from "./service/app.service";
import { TypeOrmModule } from "@nestjs/typeorm";
import { configService } from "./presentation/config/config.service";

@Module({
  imports: [TypeOrmModule.forRoot(configService.getTypeOrmConfig())],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}
