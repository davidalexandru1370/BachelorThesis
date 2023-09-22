import { AutoMap } from "@automapper/classes";

export class CreateDocumentCommand {
  @AutoMap()
  storageUrl: string;
  @AutoMap()
  createdAt: Date;
}
