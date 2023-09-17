"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Init1694955526348 = void 0;
class Init1694955526348 {
    constructor() {
        this.name = 'Init1694955526348';
    }
    async up(queryRunner) {
        await queryRunner.query(`CREATE TABLE "user" ("id" uuid NOT NULL DEFAULT uuid_generate_v4(), "email" character varying(255) NOT NULL, "password" character varying(255) NOT NULL, CONSTRAINT "UQ_e12875dfb3b1d92d7d7c5377e22" UNIQUE ("email"), CONSTRAINT "PK_cace4a159ff9f2512dd42373760" PRIMARY KEY ("id"))`);
    }
    async down(queryRunner) {
        await queryRunner.query(`DROP TABLE "user"`);
    }
}
exports.Init1694955526348 = Init1694955526348;
//# sourceMappingURL=1694955526348-init.js.map