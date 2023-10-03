using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SealabAPI.DataAccess.Migrations
{
    public partial class BindPAWtoModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_preliminary_assignment_answer_preliminary_assignment_questi",
                table: "preliminary_assignment_answer");

            migrationBuilder.RenameColumn(
                name: "id_question",
                table: "preliminary_assignment_answer",
                newName: "id_module");

            migrationBuilder.RenameColumn(
                name: "answer",
                table: "preliminary_assignment_answer",
                newName: "file_path");

            migrationBuilder.RenameIndex(
                name: "ix_preliminary_assignment_answer_id_question",
                table: "preliminary_assignment_answer",
                newName: "ix_preliminary_assignment_answer_id_module");

            migrationBuilder.AddColumn<DateTime>(
                name: "submit_time",
                table: "preliminary_assignment_answer",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "fk_preliminary_assignment_answer_module_id_module",
                table: "preliminary_assignment_answer",
                column: "id_module",
                principalTable: "module",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_preliminary_assignment_answer_module_id_module",
                table: "preliminary_assignment_answer");

            migrationBuilder.DropColumn(
                name: "submit_time",
                table: "preliminary_assignment_answer");

            migrationBuilder.RenameColumn(
                name: "id_module",
                table: "preliminary_assignment_answer",
                newName: "id_question");

            migrationBuilder.RenameColumn(
                name: "file_path",
                table: "preliminary_assignment_answer",
                newName: "answer");

            migrationBuilder.RenameIndex(
                name: "ix_preliminary_assignment_answer_id_module",
                table: "preliminary_assignment_answer",
                newName: "ix_preliminary_assignment_answer_id_question");

            migrationBuilder.AddForeignKey(
                name: "fk_preliminary_assignment_answer_preliminary_assignment_questi",
                table: "preliminary_assignment_answer",
                column: "id_question",
                principalTable: "preliminary_assignment_question",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
