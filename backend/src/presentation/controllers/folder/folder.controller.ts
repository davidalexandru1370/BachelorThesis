import { Mapper } from "@automapper/core";
import {
  Body,
  Controller,
  Get,
  HttpCode,
  HttpStatus,
  Post,
} from "@nestjs/common";
import { InjectMapper } from "@timonmasberg/automapper-nestjs";
import { plainToClass } from "class-transformer";
import { CreateFolderRequest } from "src/presentation/entities/requests/folder/create.folder.request";
import { CreateFolderCommand } from "src/service/entities/folder/create.folder.command";
import { FolderService } from "src/service/services/folder/folder.service";

@Controller("api/folder")
export class FolderController {
  constructor(private readonly folderService: FolderService) {}

  @Post()
  @HttpCode(HttpStatus.OK)
  @HttpCode(HttpStatus.BAD_REQUEST)
  async createFolder(@Body() createFolderRequest: CreateFolderRequest) {
    const createFolderCommand = plainToClass(
      CreateFolderCommand,
      createFolderRequest
    );

    const createdAt = new Date();
    createFolderCommand.createdAt = createdAt;
    createFolderCommand.createdBy = "david";

    createFolderCommand.documents.forEach((document) => {
      document.createdAt = createdAt;
    });

    return await this.folderService.createFolder(createFolderCommand);
  }
}
