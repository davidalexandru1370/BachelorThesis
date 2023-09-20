import { Injectable } from "@nestjs/common";
import { InjectRepository } from "@nestjs/typeorm";
import { Document } from "src/core/domain/document.entity";
import { Repository } from "typeorm";

@Injectable()
export class DocumentService {
  constructor(
    @InjectRepository(Document)
    private documentRepository: Repository<Document>
  ) {}

  async getDocumentByFolderId(folderId: string): Promise<Document[]> {
    return await this.documentRepository.find({
      where: {},
    });
  }
}
