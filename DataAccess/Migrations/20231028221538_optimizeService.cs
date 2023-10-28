using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SealabAPI.DataAccess.Migrations
{
    public partial class optimizeService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_student_id_user",
                table: "student");

            migrationBuilder.DropIndex(
                name: "ix_assistant_id_user",
                table: "assistant");

            migrationBuilder.CreateIndex(
                name: "ix_student_id_user",
                table: "student",
                column: "id_user",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_assistant_id_user",
                table: "assistant",
                column: "id_user",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_student_id_user",
                table: "student");

            migrationBuilder.DropIndex(
                name: "ix_assistant_id_user",
                table: "assistant");

            migrationBuilder.CreateIndex(
                name: "ix_student_id_user",
                table: "student",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "ix_assistant_id_user",
                table: "assistant",
                column: "id_user");
        }
    }
}
