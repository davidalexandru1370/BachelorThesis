import { Column } from "typeorm";

export interface ISoftDelete {
  isDeleted: boolean;
  deletedAt?: Date;
}
