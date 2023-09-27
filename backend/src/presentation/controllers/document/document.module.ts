import {Module} from '@nestjs/common';
import {DocumentController} from './document.controller';
import {DocumentService} from "../../../service/services/document/documents.service";
import {TypeOrmModule} from "@nestjs/typeorm";
import {Document} from "../../../core/domain/document.entity";

@Module({
  imports: [TypeOrmModule.forFeature([Document])],
  controllers: [DocumentController],
  providers: [DocumentService],
})
export class DocumentModule {}
