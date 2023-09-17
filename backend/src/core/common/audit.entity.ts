import { Column, PrimaryGeneratedColumn } from "typeorm";

export abstract class Audit {
  @PrimaryGeneratedColumn("uuid")
  id: string;

  @Column()
  createdAt: Date;

  @Column({ nullable: true })
  updatedAt?: Date;
}
