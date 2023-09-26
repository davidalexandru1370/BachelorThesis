import {
  Injectable,
  NotFoundException,
  UnauthorizedException,
} from "@nestjs/common";
import { InjectRepository } from "@nestjs/typeorm";
import { plainToClass } from "class-transformer";
import { Folder } from "src/core/domain/folder.entity";
import { CreateFolderCommand } from "src/service/entities/folder/create.folder.command";
import { Repository } from "typeorm";
import { ApiErrorCodes } from "../../../core/constants/i18n";

@Injectable()
export class FolderService {
  constructor(
    @InjectRepository(Folder)
    private folderRepository: Repository<Folder>,
  ) {}

  async createFolder(createFolderCommand: CreateFolderCommand) {
    const folder: Folder = plainToClass(Folder, createFolderCommand);
    return await this.folderRepository.save(folder);
  }

  async getAllFoldersWithDocumentsByOwnerId(id: string): Promise<Folder[]> {
    return this.folderRepository.find({
      where: {
        owner: {
          id: id,
        },
      },
      relations: {
        documents: true,
      },
    });
  }

  async getFolderWithDocuments(id: string, ownerId: string): Promise<Folder> {
    const folder: Folder = await this.folderRepository.findOne({
      relations: {
        owner: true,
        documents: true,
      },
      where: {
        id: id,
      },
    });

    if (folder === null) {
      throw new NotFoundException(ApiErrorCodes.FOLDER_DOES_NOT_EXISTS);
    }

    if (folder.owner.id === ownerId) {
      throw new UnauthorizedException();
    }

    return folder;
  }

  async deleteFolder(id: string) {
    const folder: Folder = await this.folderRepository.findOne({
      relations: {
        documents: true,
      },
      where: {
        id: id,
      },
    });

    if (folder === null) {
      throw new NotFoundException(ApiErrorCodes.FOLDER_DOES_NOT_EXISTS);
    }

    await this.folderRepository.softRemove(folder);
  }
}
