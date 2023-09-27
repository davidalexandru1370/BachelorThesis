import {Injectable, NotFoundException} from "@nestjs/common";
import {InjectRepository} from "@nestjs/typeorm";
import {Document} from "src/core/domain/document.entity";
import {Repository} from "typeorm";
import {ApiErrorCodes} from "../../../core/constants/i18n";
import {plainToInstance} from "class-transformer";
import {CreateDocumentCommand} from "../../entities/document/create.document.command";
import {DocumentType} from "../../../core/constants/documentType.entity";

@Injectable()
export class DocumentService {
    constructor(
        @InjectRepository(Document)
        private documentRepository: Repository<Document>
    ) {
    }

    async getDocumentsByFolderId(folderId: string): Promise<Document[]> {
        const documents: Document[] = await this.documentRepository.find({
            where: {
                folder: {
                    id: folderId
                }
            },
        });

        if (documents === null) {
            throw new NotFoundException(ApiErrorCodes.FOLDER_DOES_NOT_EXISTS);
        }

        return documents;
    }

    async addDocument(createDocumentCommand: CreateDocumentCommand): Promise<Document> {
      let document :Document = plainToInstance(Document, createDocumentCommand)
      document = this.documentRepository.create(document);
      document.documentType = DocumentType.NOT_COMPUTED;
      return await this.documentRepository.save(document);
    }
}
