import {IsString, IsUUID, MinLength} from "class-validator";

export class CreateDocumentRequest {
  @IsString()
  @MinLength(50)
  storageUrl: string;
  @IsUUID()
  folderId: string;
}
