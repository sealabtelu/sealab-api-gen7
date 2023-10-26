using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SealabAPI.DataAccess.Migrations
{
    public partial class preTestAnswerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pre_test_answer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_option = table.Column<Guid>(type: "uuid", nullable: false),
                    id_student = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pre_test_answer", x => x.id);
                    table.ForeignKey(
                        name: "fk_pre_test_answer_pre_test_option_id_option",
                        column: x => x.id_option,
                        principalTable: "pre_test_option",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pre_test_answer_student_id_student",
                        column: x => x.id_student,
                        principalTable: "student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_pre_test_answer_id_option",
                table: "pre_test_answer",
                column: "id_option");

            migrationBuilder.CreateIndex(
                name: "ix_pre_test_answer_id_student",
                table: "pre_test_answer",
                column: "id_student");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pre_test_answer");
        }
    }
}
