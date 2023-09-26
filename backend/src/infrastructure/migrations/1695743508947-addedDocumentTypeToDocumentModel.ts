import { MigrationInterface, QueryRunner } from "typeorm";

export class AddedDocumentTypeToDocumentModel1695743508947 implements MigrationInterface {
    name = 'AddedDocumentTypeToDocumentModel1695743508947'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "document" ADD "documentType" integer NOT NULL DEFAULT '0'`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "document" DROP COLUMN "documentType"`);
    }

}
