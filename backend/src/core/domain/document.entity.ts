import { Column, Entity } from "typeorm";
import { Audit } from "../common/audit.entity";

@Entity()
export class Document extends Audit {
  @Column({ type: "varchar", length: 255 })
  storageUrl: string;
}
