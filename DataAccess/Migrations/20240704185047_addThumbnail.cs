using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SealabAPI.DataAccess.Migrations
{
    public partial class addThumbnail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "read_time",
                table: "post",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "summary",
                table: "post",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "thumbnail_url",
                table: "post",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "read_time",
                table: "post");

            migrationBuilder.DropColumn(
                name: "summary",
                table: "post");

            migrationBuilder.DropColumn(
                name: "thumbnail_url",
                table: "post");
        }
    }
}
