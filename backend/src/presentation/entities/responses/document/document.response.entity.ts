import { Expose, Transform } from "class-transformer";
import { DocumentType } from "../../../../core/constants/documentType.entity";

export class DocumentInfoResponse {
  @Expose()
  id: string;
  @Expose()
  storageUrl: string;
  @Expose()
  @Transform((value) => {
    return value.value === undefined ? DocumentType.NOT_COMPUTED : value;
  })
  type: DocumentType;
}
