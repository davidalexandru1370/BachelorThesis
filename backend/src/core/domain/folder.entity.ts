import { Column, Entity, OneToMany, PrimaryGeneratedColumn } from "typeorm";
import { Audit } from "../common/audit.entity";
import { Document } from "./document.entity";

@Entity()
export class Folder extends Audit {
  @PrimaryGeneratedColumn("uuid")
  id: string;

  @Column({ type: "varchar", length: 255 })
  storageUrl: string;

  @OneToMany(() => Document, (document) => document.folder)
  documents: Document[];
}
