import {
  AfterSoftRemove,
  Column,
  DeleteDateColumn,
  Entity,
  ManyToOne,
  OneToMany,
  PrimaryGeneratedColumn,
} from "typeorm";
import { Audit } from "../common/audit.entity";
import { Document } from "./document.entity";
import { ISoftDelete } from "../interfaces/softDelete.entity";
import { User } from "./user.entity";

@Entity()
export class Folder extends Audit implements ISoftDelete {
  @PrimaryGeneratedColumn("uuid")
  id: string;

  @Column({ type: "varchar", length: 255 })
  storageUrl: string;

  @OneToMany(() => Document, (document: Document) => document.folder, {
    cascade: true,
    onDelete: "CASCADE",
  })
  documents: Document[];
  @Column({
    nullable: true,
  })
  @DeleteDateColumn()
  deletedAt?: Date | undefined;

  @Column({
    default: false,
  })
  isDeleted: boolean = false;

  @ManyToOne(() => User, (user: User) => user.folders)
  owner: User;
}
