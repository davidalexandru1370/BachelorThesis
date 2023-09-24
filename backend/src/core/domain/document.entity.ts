import {
  Column,
  DeleteDateColumn,
  Entity,
  ManyToOne,
  JoinColumn,
} from "typeorm";
import { Audit } from "../common/audit.entity";
import { Folder } from "./folder.entity";
import { ISoftDelete } from "../interfaces/softDelete.entity";

@Entity()
export class Document extends Audit implements ISoftDelete {
  @Column({ type: "varchar", length: 255 })
  storageUrl: string;

  @ManyToOne(() => Folder, (folder) => folder.documents, {
    orphanedRowAction: "delete",
    nullable: false,
  })
  @JoinColumn()
  folder: Folder;

  @Column({
    nullable: true,
  })
  @DeleteDateColumn()
  deletedAt?: string | undefined;

  @Column({
    default: false,
  })
  isDeleted: boolean;
}
