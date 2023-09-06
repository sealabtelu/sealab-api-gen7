using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SealabAPI.DataAccess.Migrations
{
    public partial class ModuleMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "module",
                table: "preliminary_assignment_question");

            migrationBuilder.DropColumn(
                name: "module",
                table: "pre_test_question");

            migrationBuilder.AddColumn<Guid>(
                name: "id_module",
                table: "preliminary_assignment_question",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "id_module",
                table: "pre_test_question",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "module",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    seelabs_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_module", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_preliminary_assignment_question_id_module",
                table: "preliminary_assignment_question",
                column: "id_module");

            migrationBuilder.CreateIndex(
                name: "ix_pre_test_question_id_module",
                table: "pre_test_question",
                column: "id_module");

            migrationBuilder.AddForeignKey(
                name: "fk_pre_test_question_module_id_module",
                table: "pre_test_question",
                column: "id_module",
                principalTable: "module",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_preliminary_assignment_question_module_id_module",
                table: "preliminary_assignment_question",
                column: "id_module",
                principalTable: "module",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pre_test_question_module_id_module",
                table: "pre_test_question");

            migrationBuilder.DropForeignKey(
                name: "fk_preliminary_assignment_question_module_id_module",
                table: "preliminary_assignment_question");

            migrationBuilder.DropTable(
                name: "module");

            migrationBuilder.DropIndex(
                name: "ix_preliminary_assignment_question_id_module",
                table: "preliminary_assignment_question");

            migrationBuilder.DropIndex(
                name: "ix_pre_test_question_id_module",
                table: "pre_test_question");

            migrationBuilder.DropColumn(
                name: "id_module",
                table: "preliminary_assignment_question");

            migrationBuilder.DropColumn(
                name: "id_module",
                table: "pre_test_question");

            migrationBuilder.AddColumn<int>(
                name: "module",
                table: "preliminary_assignment_question",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "module",
                table: "pre_test_question",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
