import { Test, TestingModule } from '@nestjs/testing';
import { DocumentController } from './document.controller';
import {DocumentService} from "../../../service/services/document/documents.service";

describe('DocumentController', () => {
  let controller: DocumentController;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [DocumentController],
      providers: [DocumentService],
    }).compile();

    controller = module.get<DocumentController>(DocumentController);
  });

  it('should be defined', () => {
    expect(controller).toBeDefined();
  });
});
