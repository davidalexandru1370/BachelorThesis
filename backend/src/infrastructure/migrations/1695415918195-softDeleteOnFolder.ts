import { MigrationInterface, QueryRunner } from "typeorm";

export class SoftDeleteOnFolder1695415918195 implements MigrationInterface {
    name = 'SoftDeleteOnFolder1695415918195'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "folder" ADD "deletedAt" TIMESTAMP NOT NULL`);
        await queryRunner.query(`ALTER TABLE "folder" ADD "isDeleted" boolean NOT NULL`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "folder" DROP COLUMN "isDeleted"`);
        await queryRunner.query(`ALTER TABLE "folder" DROP COLUMN "deletedAt"`);
    }

}
