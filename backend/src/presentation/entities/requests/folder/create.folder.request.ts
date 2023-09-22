import { IsString, MinLength } from "class-validator";
import { CreateDocumentRequest } from "../document/create.document.request";

export class CreateFolderRequest {
  @IsString()
  @MinLength(50)
  storageUrl: string;
  documents: CreateDocumentRequest[];
}
