import { Column, DeleteDateColumn, Entity, ManyToOne } from "typeorm";
import { Audit } from "../common/audit.entity";
import { Folder } from "./folder.entity";
import { ISoftDelete } from "../interfaces/softDelete.entity";

@Entity()
export class Document extends Audit implements ISoftDelete {
  @Column({ type: "varchar", length: 255 })
  storageUrl: string;

  @ManyToOne(() => Folder, (folder) => folder.documents)
  folder: Folder;

  @DeleteDateColumn()
  @Column({
    nullable: true,
  })
  deletedAt?: Date | undefined;

  @Column({
    default: false,
  })
  isDeleted: boolean;
}
