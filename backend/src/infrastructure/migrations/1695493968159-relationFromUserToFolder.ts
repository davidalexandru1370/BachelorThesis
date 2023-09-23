import { MigrationInterface, QueryRunner } from "typeorm";

export class RelationFromUserToFolder1695493968159 implements MigrationInterface {
    name = 'RelationFromUserToFolder1695493968159'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "folder" ADD "ownerId" uuid`);
        await queryRunner.query(`ALTER TABLE "folder" ADD CONSTRAINT "FK_e09b8e7d4818dd263dde45bbecb" FOREIGN KEY ("ownerId") REFERENCES "user"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "folder" DROP CONSTRAINT "FK_e09b8e7d4818dd263dde45bbecb"`);
        await queryRunner.query(`ALTER TABLE "folder" DROP COLUMN "ownerId"`);
    }

}
