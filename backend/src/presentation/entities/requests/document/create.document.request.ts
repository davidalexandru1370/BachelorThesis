import { IsString, MinLength } from "class-validator";

export class CreateDocumentRequest {
  @IsString()
  @MinLength(50)
  storageUrl: string;
}
