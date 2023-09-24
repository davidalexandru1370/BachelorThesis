import { MigrationInterface, QueryRunner } from "typeorm";

export class CascadeOnDeleteFolder1695547705162 implements MigrationInterface {
    name = 'CascadeOnDeleteFolder1695547705162'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "document" DROP CONSTRAINT "FK_76b187510eda9c862f9944808a8"`);
        await queryRunner.query(`ALTER TABLE "document" ALTER COLUMN "folderId" SET NOT NULL`);
        await queryRunner.query(`ALTER TABLE "document" ADD CONSTRAINT "FK_76b187510eda9c862f9944808a8" FOREIGN KEY ("folderId") REFERENCES "folder"("id") ON DELETE CASCADE ON UPDATE NO ACTION`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "document" DROP CONSTRAINT "FK_76b187510eda9c862f9944808a8"`);
        await queryRunner.query(`ALTER TABLE "document" ALTER COLUMN "folderId" DROP NOT NULL`);
        await queryRunner.query(`ALTER TABLE "document" ADD CONSTRAINT "FK_76b187510eda9c862f9944808a8" FOREIGN KEY ("folderId") REFERENCES "folder"("id") ON DELETE CASCADE ON UPDATE NO ACTION`);
    }

}
