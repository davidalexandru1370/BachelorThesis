import { DocumentInfoResponse } from "../document/document.response.entity";
import { Expose, Type } from "class-transformer";

export class FolderInfoResponse {
  @Expose()
  id: string;
  @Expose()
  storageUrl: string;
  @Expose()
  @Type(() => DocumentInfoResponse)
  documents: DocumentInfoResponse[];
}
