"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.DatabaseModule = exports.databaseProviders = exports.connectionSource = void 0;
const common_1 = require("@nestjs/common");
const providers_1 = require("./src/core/constants/providers");
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
exports.databaseProviders = [
    {
        provide: providers_1.Providers.DATA_SOURCE,
        useFactory: async () => {
            return exports.connectionSource.initialize();
        },
    },
];
let DatabaseModule = class DatabaseModule {
};
exports.DatabaseModule = DatabaseModule;
exports.DatabaseModule = DatabaseModule = __decorate([
    (0, common_1.Module)({
        providers: [...exports.databaseProviders],
        exports: [...exports.databaseProviders],
    })
], DatabaseModule);
//# sourceMappingURL=ormconfig.js.map