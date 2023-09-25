import { CreateDocumentCommand } from "../document/create.document.command";
import { User } from "../../../core/domain/user.entity";

export class CreateFolderCommand {
  storageUrl: string;
  owner: Partial<User>;
  createdAt: Date;
  documents: CreateDocumentCommand[];
}
