import { Module, Provider } from "@nestjs/common";
import { DataSource } from "typeorm";

require("dotenv").config();

export const connectionSource = new DataSource({
  type: "postgres",
  host: process.env.POSTGRES_HOST,
  port: parseInt(process.env.POSTGRES_PORT),
  username: process.env.POSTGRES_USER,
  password: process.env.POSTGRES_PASSWORD,
  database: process.env.POSTGRES_DATABASE,
  logging: true,
  synchronize: false,
  entities: ["dist/**/*.entity{.ts,.js}"],
  migrations: ["src/infrastructure/migrations/**"],
});
