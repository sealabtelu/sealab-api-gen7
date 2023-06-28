using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SealabAPI.DataAccess.Migrations
{
    public partial class PAQMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "preliminary_assignment_question",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    module = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "text", nullable: true),
                    text = table.Column<string>(type: "text", nullable: true),
                    answer_key = table.Column<string>(type: "text", nullable: true),
                    file_path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_preliminary_assignment_question", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "preliminary_assignment_answer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_user = table.Column<Guid>(type: "uuid", nullable: false),
                    id_question = table.Column<Guid>(type: "uuid", nullable: false),
                    answer = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_preliminary_assignment_answer", x => x.id);
                    table.ForeignKey(
                        name: "fk_preliminary_assignment_answer_preliminary_assignment_questi",
                        column: x => x.id_question,
                        principalTable: "preliminary_assignment_question",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_preliminary_assignment_answer_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_preliminary_assignment_answer_id_question",
                table: "preliminary_assignment_answer",
                column: "id_question");

            migrationBuilder.CreateIndex(
                name: "ix_preliminary_assignment_answer_id_user",
                table: "preliminary_assignment_answer",
                column: "id_user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "preliminary_assignment_answer");

            migrationBuilder.DropTable(
                name: "preliminary_assignment_question");
        }
    }
}
