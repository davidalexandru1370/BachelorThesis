"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.DocumentTable1694969549746 = void 0;
class DocumentTable1694969549746 {
    constructor() {
        this.name = "DocumentTable1694969549746";
    }
    async up(queryRunner) {
        await queryRunner.query(`CREATE TABLE "document" ("id" uuid NOT NULL DEFAULT uuid_generate_v4(), "createdAt" TIMESTAMP NOT NULL, "updatedAt" TIMESTAMP, "storageUrl" character varying(255) NOT NULL, CONSTRAINT "PK_e57d3357f83f3cdc0acffc3d777" PRIMARY KEY ("id"))`);
    }
    async down(queryRunner) {
        await queryRunner.query(`DROP TABLE "document"`);
    }
}
exports.DocumentTable1694969549746 = DocumentTable1694969549746;
//# sourceMappingURL=1694969549746-documentTable.js.map