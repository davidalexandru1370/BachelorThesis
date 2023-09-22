import { Module } from "@nestjs/common";
import { TypeOrmModule } from "@nestjs/typeorm";
import { Folder } from "src/core/domain/folder.entity";
import { FolderService } from "src/service/services/folder/folder.service";
import { FolderController } from "./folder.controller";
import { Document } from "src/core/domain/document.entity";

@Module({
  imports: [TypeOrmModule.forFeature([Folder, Document])],
  providers: [FolderService],
  controllers: [FolderController],
})
export class FolderModule {}
