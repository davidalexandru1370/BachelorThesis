import { MigrationInterface, QueryRunner } from "typeorm";

export class FolderEntityAndNavigationPaths1695241070578 implements MigrationInterface {
    name = 'FolderEntityAndNavigationPaths1695241070578'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`CREATE TABLE "folder" ("id" uuid NOT NULL DEFAULT uuid_generate_v4(), "createdAt" TIMESTAMP NOT NULL, "updatedAt" TIMESTAMP, "storageUrl" character varying(255) NOT NULL, CONSTRAINT "PK_6278a41a706740c94c02e288df8" PRIMARY KEY ("id"))`);
        await queryRunner.query(`ALTER TABLE "document" ADD "folderId" uuid`);
        await queryRunner.query(`ALTER TABLE "document" ADD CONSTRAINT "FK_76b187510eda9c862f9944808a8" FOREIGN KEY ("folderId") REFERENCES "folder"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "document" DROP CONSTRAINT "FK_76b187510eda9c862f9944808a8"`);
        await queryRunner.query(`ALTER TABLE "document" DROP COLUMN "folderId"`);
        await queryRunner.query(`DROP TABLE "folder"`);
    }

}
