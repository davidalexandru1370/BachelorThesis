import { Injectable } from "@nestjs/common";
import { InjectRepository } from "@nestjs/typeorm";
import { plainToClass } from "class-transformer";
import { Document } from "src/core/domain/document.entity";
import { Folder } from "src/core/domain/folder.entity";
import { CreateFolderCommand } from "src/service/entities/folder/create.folder.command";
import { Repository } from "typeorm";

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
