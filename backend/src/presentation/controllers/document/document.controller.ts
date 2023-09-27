import {Body, Controller, Post, UseGuards} from '@nestjs/common';
import {DocumentService} from "../../../service/services/document/documents.service";
import {CreateDocumentRequest} from "../../entities/requests/document/create.document.request";
import {CreateDocumentCommand} from "../../../service/entities/document/create.document.command";
import {plainToInstance} from "class-transformer";
import {Document} from "../../../core/domain/document.entity";
import {AuthGuard} from "@nestjs/passport";

@UseGuards(AuthGuard("jwt"))
@Controller('api/document')
export class DocumentController {
  constructor(private readonly documentService: DocumentService) {}

  @Post()
  async create(@Body() createDocumentRequest: CreateDocumentRequest): Promise<Document> {
    let createDocumentCommand: CreateDocumentCommand = plainToInstance(CreateDocumentCommand, createDocumentRequest);
    createDocumentCommand.createdAt = new Date()
    return await this.documentService.addDocument(createDocumentCommand);
  }
  //
  // @Get()
  // findAll() {
  //   return this.documentService.findAll();
  // }
  //
  // @Get(':id')
  // findOne(@Param('id') id: string) {
  //   return this.documentService.findOne(+id);
  // }
  //
  // @Patch(':id')
  // update(@Param('id') id: string, @Body() updateDocumentDto: UpdateDocumentDto) {
  //   return this.documentService.update(+id, updateDocumentDto);
  // }
  //
  // @Delete(':id')
  // remove(@Param('id') id: string) {
  //   return this.documentService.remove(+id);
  // }
}
