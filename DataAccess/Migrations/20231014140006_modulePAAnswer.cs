using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SealabAPI.DataAccess.Migrations
{
    public partial class modulePAAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "preliminary_assignment_question_id",
                table: "preliminary_assignment_answer",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_preliminary_assignment_answer_preliminary_assignment_questi",
                table: "preliminary_assignment_answer",
                column: "preliminary_assignment_question_id");

            migrationBuilder.AddForeignKey(
                name: "fk_preliminary_assignment_answer_preliminary_assignment_questi",
                table: "preliminary_assignment_answer",
                column: "preliminary_assignment_question_id",
                principalTable: "preliminary_assignment_question",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_preliminary_assignment_answer_preliminary_assignment_questi",
                table: "preliminary_assignment_answer");

            migrationBuilder.DropIndex(
                name: "ix_preliminary_assignment_answer_preliminary_assignment_questi",
                table: "preliminary_assignment_answer");

            migrationBuilder.DropColumn(
                name: "preliminary_assignment_question_id",
                table: "preliminary_assignment_answer");
        }
    }
}
