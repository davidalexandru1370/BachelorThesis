import { AutoMap } from "@automapper/classes";
import { CreateDocumentCommand } from "../document/create.document.command";

export class CreateFolderCommand {
  storageUrl: string;
  createdBy: string;
  createdAt: Date;
  documents: CreateDocumentCommand[];
}
