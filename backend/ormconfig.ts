import { DataSource } from "typeorm";

export const connectionSource = new DataSource({
  type: "postgres",
  host: "localhost",
  port: 5432,
  username: "postgres",
  password: "postgres",
  database: "sdia",
  logging: true,
  synchronize: false,
  entities: ["dist/**/*.entity{.ts,.js}"],
  migrations: ["src/infrastructure/migrations/**"],
});
