import { Expose } from "class-transformer";
import { DocumentType } from "../../../../core/common/enums/documentType.entity";

export class DocumentInfoResponse {
  @Expose()
  id: string;
  @Expose()
  storageUrl: string;
  @Expose()
  type: DocumentType;
}
