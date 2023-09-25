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
  UseGuards,
} from "@nestjs/common";
import { AuthGuard } from "@nestjs/passport";
import { plainToClass } from "class-transformer";
import { CreateFolderRequest } from "src/presentation/entities/requests/folder/create.folder.request";
import { CreateFolderCommand } from "src/service/entities/folder/create.folder.command";
import { FolderService } from "src/service/services/folder/folder.service";

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
    createFolderCommand.ownerId = req.user.userId;
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

  @HttpCode(HttpStatus.NO_CONTENT)
  @HttpCode(HttpStatus.OK)
  @Get("list")
  async getFolderWithDocumentsByOwnerId(
    @Req()
    req: Request & {
      user: {
        email: string;
        userId: string;
      };
    },
  ) {
    const folders =
      await this.folderService.getAllFoldersWithDocumentsByOwnerId(
        req.user.userId,
      );
    if (folders.length === 0) {
      return;
    }
  }
}
