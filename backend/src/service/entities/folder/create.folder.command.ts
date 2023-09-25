import { AutoMap } from "@automapper/classes";
import { CreateDocumentCommand } from "../document/create.document.command";

export class CreateFolderCommand {
  storageUrl: string;
  ownerId: string;
  createdAt: Date;
  documents: CreateDocumentCommand[];
}
