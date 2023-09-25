import { Column } from "typeorm";

export interface ISoftDelete {
  deletedAt?: string | undefined;
}
