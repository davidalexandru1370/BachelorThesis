import { MigrationInterface, QueryRunner } from "typeorm";

export class ChangeDateToString1695557888654 implements MigrationInterface {
    name = 'ChangeDateToString1695557888654'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "document" DROP CONSTRAINT "FK_76b187510eda9c862f9944808a8"`);
        await queryRunner.query(`ALTER TABLE "document" ADD CONSTRAINT "FK_76b187510eda9c862f9944808a8" FOREIGN KEY ("folderId") REFERENCES "folder"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "document" DROP CONSTRAINT "FK_76b187510eda9c862f9944808a8"`);
        await queryRunner.query(`ALTER TABLE "document" ADD CONSTRAINT "FK_76b187510eda9c862f9944808a8" FOREIGN KEY ("folderId") REFERENCES "folder"("id") ON DELETE CASCADE ON UPDATE NO ACTION`);
    }

}
