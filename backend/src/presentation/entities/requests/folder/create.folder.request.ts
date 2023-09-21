import { CreateDocumentRequest } from "../document/create.document.request";

export class CreateFolderRequest {
  storageUrl: string;
  documents: CreateDocumentRequest[];
}
