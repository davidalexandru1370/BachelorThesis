import { AutoMap } from "@automapper/classes";
import { CreateDocumentCommand } from "../document/create.document.command";

export class CreateFolderCommand {
  @AutoMap()
  storageUrl: string;
  @AutoMap()
  createdBy: string;
  @AutoMap()
  createdAt: Date;
  @AutoMap(() => CreateDocumentCommand)
  documents: CreateDocumentCommand[];
}
