import {
  Body,
  Controller,
  Delete,
  Get,
  HttpCode,
  HttpStatus,
  Param,
  Post,
  Req,
  Res,
  UseGuards,
} from "@nestjs/common";
import { AuthGuard } from "@nestjs/passport";
import { plainToClass, plainToInstance } from "class-transformer";
import { CreateFolderRequest } from "src/presentation/entities/requests/folder/create.folder.request";
import { CreateFolderCommand } from "src/service/entities/folder/create.folder.command";
import { FolderService } from "src/service/services/folder/folder.service";
import { Response } from "express";
import { FolderInfoResponse } from "../../entities/responses/folder/folder.response.entity";
import { Folder } from "../../../core/domain/folder.entity";

@UseGuards(AuthGuard("jwt"))
@Controller("api/folder")
export class FolderController {
  constructor(private readonly folderService: FolderService) {}

  @Post()
  @HttpCode(HttpStatus.OK)
  @HttpCode(HttpStatus.BAD_REQUEST)
  async createFolder(
    @Body() createFolderRequest: CreateFolderRequest,
    @Req()
    req: Request & {
      user: {
        email: string;
        userId: string;
      };
    },
  ) {
    const createFolderCommand: CreateFolderCommand = plainToClass(
      CreateFolderCommand,
      createFolderRequest,
    );

    const createdAt = new Date();
    createFolderCommand.createdAt = createdAt;
    createFolderCommand.owner = {
      id: req.user.userId,
    };
    createFolderCommand.documents.forEach((document) => {
      document.createdAt = createdAt;
    });

    return await this.folderService.createFolder(createFolderCommand);
  }

  @Delete(":id")
  async deleteFolder(@Param("id") id: string) {
    await this.folderService.deleteFolder(id);
  }

  @Get(":id")
  async getFolderWithDocuments(
    @Param("id") id: string,
    @Req()
    req: Request & {
      user: {
        email: string;
        userId: string;
      };
    },
  ) {
    return await this.folderService.getFolderWithDocuments(id, req.user.userId);
  }

  @Get()
  async getFolderWithDocumentsByOwnerId(
    @Req()
    req: Request & {
      user: {
        email: string;
        userId: string;
      };
    },
    @Res()
    res: Response,
  ): Promise<FolderInfoResponse[]> {
    const folders: Folder[] =
      await this.folderService.getAllFoldersWithDocumentsByOwnerId(
        req.user.userId,
      );

    const response = plainToInstance(FolderInfoResponse, folders, {
      excludeExtraneousValues: true,
    });
    if (folders.length === 0) {
      res.status(HttpStatus.NO_CONTENT).send();
      return;
    }

    res.status(HttpStatus.OK).send(response);
  }
}
