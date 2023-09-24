import { Injectable, NotFoundException } from "@nestjs/common";
import { InjectRepository } from "@nestjs/typeorm";
import { plainToClass } from "class-transformer";
import { Document } from "src/core/domain/document.entity";
import { Folder } from "src/core/domain/folder.entity";
import { CreateFolderCommand } from "src/service/entities/folder/create.folder.command";
import { FindOptionsWhere, Repository, SelectQueryBuilder } from "typeorm";
import { ApiErrorCodes } from "../../../core/constants/i18n";

@Injectable()
export class FolderService {
  constructor(
    @InjectRepository(Folder)
    private folderRepository: Repository<Folder>,
  ) {}

  async createFolder(createFolderCommand: CreateFolderCommand) {
    const folder = plainToClass(Folder, createFolderCommand);
    return await this.folderRepository.save(folder);
  }

  async getFolderWithDocuments(id: string): Promise<Folder> {
    const folder = await this.folderRepository.findOne({
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
