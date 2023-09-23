import { MigrationInterface, QueryRunner } from "typeorm";

export class SoftDeleteOnDocument1695494243277 implements MigrationInterface {
    name = 'SoftDeleteOnDocument1695494243277'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "document" ADD "deletedAt" TIMESTAMP`);
        await queryRunner.query(`ALTER TABLE "document" ADD "isDeleted" boolean NOT NULL DEFAULT false`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "document" DROP COLUMN "isDeleted"`);
        await queryRunner.query(`ALTER TABLE "document" DROP COLUMN "deletedAt"`);
    }

}
