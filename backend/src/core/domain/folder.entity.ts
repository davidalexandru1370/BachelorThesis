import {
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
import { Exclude, Expose, Transform, Type } from "class-transformer";

@Entity()
export class Folder extends Audit implements ISoftDelete {
  @Expose()
  @PrimaryGeneratedColumn("uuid")
  id: string;

  @Expose()
  @Column({ type: "varchar", length: 255 })
  storageUrl: string;

  @Expose()
  @Type(() => Document)
  @OneToMany(() => Document, (document: Document) => document.folder, {
    cascade: ["insert", "update", "soft-remove"],
    onUpdate: "CASCADE",
    onDelete: "CASCADE",
  })
  documents: Document[];
  @Column({
    nullable: true,
  })
  @Exclude()
  @DeleteDateColumn()
  deletedAt?: string | undefined;

  @Expose()
  @ManyToOne(() => User, (user: User) => user.folders)
  owner: User;

  constructor(partial: Partial<Folder>) {
    super();
    Object.assign(this, partial);
  }
}
