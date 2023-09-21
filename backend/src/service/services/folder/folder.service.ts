import { Mapper } from "@automapper/core";
import { Injectable } from "@nestjs/common";
import { InjectRepository } from "@nestjs/typeorm";
import { InjectMapper } from "@timonmasberg/automapper-nestjs";
import { plainToClass } from "class-transformer";
import { Document } from "src/core/domain/document.entity";
import { Folder } from "src/core/domain/folder.entity";
import { CreateFolderCommand } from "src/service/entities/folder/create.folder.command";
import { Repository, Transaction } from "typeorm";

@Injectable()
export class FolderService {
  constructor(
    @InjectRepository(Folder)
    private folderRepository: Repository<Folder>,
    @InjectRepository(Document)
    private documentRepository: Repository<Document>
  ) {}

  async createFolder(createFolderCommand: CreateFolderCommand) {
    let folder = plainToClass(Folder, createFolderCommand);

    const documents = await this.documentRepository.save(folder.documents);
    folder = {
      ...folder,
      documents,
    };
    let added = await this.folderRepository.save(folder);

    return added;
  }
}
