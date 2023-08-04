using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SealabAPI.DataAccess.Migrations
{
    public partial class PreTestQuestionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pre_test_question",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    module = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "text", nullable: true),
                    question = table.Column<string>(type: "text", nullable: true),
                    file_path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pre_test_question", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pre_test_option",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_question = table.Column<Guid>(type: "uuid", nullable: false),
                    option = table.Column<string>(type: "text", nullable: true),
                    is_true = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pre_test_option", x => x.id);
                    table.ForeignKey(
                        name: "fk_pre_test_option_pre_test_question_id_question",
                        column: x => x.id_question,
                        principalTable: "pre_test_question",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_pre_test_option_id_question",
                table: "pre_test_option",
                column: "id_question");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pre_test_option");

            migrationBuilder.DropTable(
                name: "pre_test_question");
        }
    }
}
