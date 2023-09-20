import { Column, Entity, ManyToOne } from "typeorm";
import { Audit } from "../common/audit.entity";
import { Folder } from "./folder.entity";

@Entity()
export class Document extends Audit {
  @Column({ type: "varchar", length: 255 })
  storageUrl: string;

  @ManyToOne(() => Folder, (folder) => folder.documents)
  folder: Folder;
}
