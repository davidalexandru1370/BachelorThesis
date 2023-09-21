import {
  Body,
  Controller,
  HttpCode,
  HttpStatus,
  Post,
  Req,
  UseGuards,
} from "@nestjs/common";
import { AuthGuard } from "@nestjs/passport";
import { plainToClass } from "class-transformer";
import { CreateFolderRequest } from "src/presentation/entities/requests/folder/create.folder.request";
import { CreateFolderCommand } from "src/service/entities/folder/create.folder.command";
import { FolderService } from "src/service/services/folder/folder.service";

@Controller("api/folder")
export class FolderController {
  constructor(private readonly folderService: FolderService) {}

  @UseGuards(AuthGuard("jwt"))
  @Post()
  @HttpCode(HttpStatus.OK)
  @HttpCode(HttpStatus.BAD_REQUEST)
  async createFolder(
    @Body() createFolderRequest: CreateFolderRequest,
    @Req()
    req: Request & {
      user: {
        email: string;
      };
    }
  ) {
    const createFolderCommand = plainToClass(
      CreateFolderCommand,
      createFolderRequest
    );

    const createdAt = new Date();
    createFolderCommand.createdAt = createdAt;
    createFolderCommand.createdBy = req.user.email;

    createFolderCommand.documents.forEach((document) => {
      document.createdAt = createdAt;
    });

    return await this.folderService.createFolder(createFolderCommand);
  }
}
