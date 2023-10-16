using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SealabAPI.DataAccess.Migrations
{
    public partial class journalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "journal_answer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_student = table.Column<Guid>(type: "uuid", nullable: false),
                    id_module = table.Column<Guid>(type: "uuid", nullable: false),
                    assistant_feedback = table.Column<string>(type: "text", nullable: true),
                    session_feedback = table.Column<string>(type: "text", nullable: true),
                    laboratory_feedback = table.Column<string>(type: "text", nullable: true),
                    file_path = table.Column<string>(type: "text", nullable: true),
                    submit_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_journal_answer", x => x.id);
                    table.ForeignKey(
                        name: "fk_journal_answer_module_id_module",
                        column: x => x.id_module,
                        principalTable: "module",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_journal_answer_student_id_student",
                        column: x => x.id_student,
                        principalTable: "student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_journal_answer_id_module",
                table: "journal_answer",
                column: "id_module");

            migrationBuilder.CreateIndex(
                name: "ix_journal_answer_id_student",
                table: "journal_answer",
                column: "id_student");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "journal_answer");
        }
    }
}
