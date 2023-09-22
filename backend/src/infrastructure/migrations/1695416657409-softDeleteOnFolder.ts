import { MigrationInterface, QueryRunner } from "typeorm";

export class SoftDeleteOnFolder1695416657409 implements MigrationInterface {
    name = 'SoftDeleteOnFolder1695416657409'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "folder" ADD "deletedAt" TIMESTAMP`);
        await queryRunner.query(`ALTER TABLE "folder" ADD "isDeleted" boolean NOT NULL DEFAULT false`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "folder" DROP COLUMN "isDeleted"`);
        await queryRunner.query(`ALTER TABLE "folder" DROP COLUMN "deletedAt"`);
    }

}
