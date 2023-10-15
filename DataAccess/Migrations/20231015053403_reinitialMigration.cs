using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SealabAPI.DataAccess.Migrations
{
    public partial class reinitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "module",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    seelabs_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    is_pa_open = table.Column<bool>(type: "boolean", nullable: false),
                    is_prt_open = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_module", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nim = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    username = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    app_token = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pre_test_question",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_module = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: true),
                    question = table.Column<string>(type: "text", nullable: true),
                    file_path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pre_test_question", x => x.id);
                    table.ForeignKey(
                        name: "fk_pre_test_question_module_id_module",
                        column: x => x.id_module,
                        principalTable: "module",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "preliminary_assignment_question",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_module = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: true),
                    question = table.Column<string>(type: "text", nullable: true),
                    answer_key = table.Column<string>(type: "text", nullable: true),
                    file_path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_preliminary_assignment_question", x => x.id);
                    table.ForeignKey(
                        name: "fk_preliminary_assignment_question_module_id_module",
                        column: x => x.id_module,
                        principalTable: "module",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assistant",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_user = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: true),
                    position = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assistant", x => x.id);
                    table.ForeignKey(
                        name: "fk_assistant_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_user = table.Column<Guid>(type: "uuid", nullable: false),
                    classroom = table.Column<string>(type: "text", nullable: true),
                    group = table.Column<int>(type: "integer", nullable: false),
                    day = table.Column<int>(type: "integer", nullable: false),
                    shift = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_student", x => x.id);
                    table.ForeignKey(
                        name: "fk_student_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "preliminary_assignment_answer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_student = table.Column<Guid>(type: "uuid", nullable: false),
                    id_module = table.Column<Guid>(type: "uuid", nullable: false),
                    file_path = table.Column<string>(type: "text", nullable: true),
                    submit_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_preliminary_assignment_answer", x => x.id);
                    table.ForeignKey(
                        name: "fk_preliminary_assignment_answer_module_id_module",
                        column: x => x.id_module,
                        principalTable: "module",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_preliminary_assignment_answer_student_id_student",
                        column: x => x.id_student,
                        principalTable: "student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_assistant_id_user",
                table: "assistant",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "ix_pre_test_option_id_question",
                table: "pre_test_option",
                column: "id_question");

            migrationBuilder.CreateIndex(
                name: "ix_pre_test_question_id_module",
                table: "pre_test_question",
                column: "id_module");

            migrationBuilder.CreateIndex(
                name: "ix_preliminary_assignment_answer_id_module",
                table: "preliminary_assignment_answer",
                column: "id_module");

            migrationBuilder.CreateIndex(
                name: "ix_preliminary_assignment_answer_id_student",
                table: "preliminary_assignment_answer",
                column: "id_student");

            migrationBuilder.CreateIndex(
                name: "ix_preliminary_assignment_question_id_module",
                table: "preliminary_assignment_question",
                column: "id_module");

            migrationBuilder.CreateIndex(
                name: "ix_student_id_user",
                table: "student",
                column: "id_user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assistant");

            migrationBuilder.DropTable(
                name: "pre_test_option");

            migrationBuilder.DropTable(
                name: "preliminary_assignment_answer");

            migrationBuilder.DropTable(
                name: "preliminary_assignment_question");

            migrationBuilder.DropTable(
                name: "pre_test_question");

            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "module");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
