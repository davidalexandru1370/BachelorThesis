import {Expose, Transform, Type} from "class-transformer";
import {Folder} from "../../../core/domain/folder.entity";

export class CreateDocumentCommand {
  @Expose()
  storageUrl: string;
  @Expose()
  createdAt: Date;
  @Expose()
  @Type(() => Folder)
  folder: Partial<Folder>
}
