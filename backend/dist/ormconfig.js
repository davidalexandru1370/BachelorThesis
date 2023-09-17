"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.connectionSource = void 0;
const typeorm_1 = require("typeorm");
exports.connectionSource = new typeorm_1.DataSource({
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
//# sourceMappingURL=ormconfig.js.map