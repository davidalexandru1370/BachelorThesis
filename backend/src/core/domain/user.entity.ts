import { Column, Entity, OneToMany, PrimaryGeneratedColumn } from "typeorm";
import { Folder } from "./folder.entity";

@Entity()
export class User {
  @PrimaryGeneratedColumn("uuid")
  id: string;

  @Column({ unique: true, type: "varchar", length: 255 })
  email: string;

  @Column({ type: "varchar", length: 255 })
  password: string;

  @OneToMany(() => Folder, (folder: Folder) => folder.owner)
  folders: Folder[];

  constructor(email: string, password: string) {
    this.email = email;
    this.password = password;
  }
}
