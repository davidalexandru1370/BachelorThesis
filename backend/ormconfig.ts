import { Module, Provider } from "@nestjs/common";
import { Providers } from "src/core/constants/providers";
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

export const databaseProviders: Array<Provider> = [
  {
    provide: Providers.DATA_SOURCE,
    useFactory: async () => {
      return connectionSource.initialize();
    },
  },
];

@Module({
  providers: [...databaseProviders],
  exports: [...databaseProviders],
})
export class DatabaseModule {}
