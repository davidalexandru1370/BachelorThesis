import { MigrationInterface, QueryRunner } from "typeorm";

export class RemovedIsDeletedField1695656809601 implements MigrationInterface {
    name = 'RemovedIsDeletedField1695656809601'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "folder" DROP COLUMN "isDeleted"`);
        await queryRunner.query(`ALTER TABLE "document" DROP COLUMN "isDeleted"`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "document" ADD "isDeleted" boolean NOT NULL DEFAULT false`);
        await queryRunner.query(`ALTER TABLE "folder" ADD "isDeleted" boolean NOT NULL DEFAULT false`);
    }

}
