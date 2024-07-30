using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SealabAPI.DataAccess.Migrations
{
    public partial class postToArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "post_category");

            migrationBuilder.CreateTable(
                name: "article_category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_article_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "article",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_assistant = table.Column<Guid>(type: "uuid", nullable: false),
                    id_category = table.Column<Guid>(type: "uuid", nullable: false),
                    thumbnail_url = table.Column<string>(type: "text", nullable: true),
                    title = table.Column<string>(type: "text", nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    summary = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_article", x => x.id);
                    table.ForeignKey(
                        name: "fk_article_article_category_id_category",
                        column: x => x.id_category,
                        principalTable: "article_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_article_assistant_id_assistant",
                        column: x => x.id_assistant,
                        principalTable: "assistant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_article_id_assistant",
                table: "article",
                column: "id_assistant");

            migrationBuilder.CreateIndex(
                name: "ix_article_id_category",
                table: "article",
                column: "id_category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article");

            migrationBuilder.DropTable(
                name: "article_category");

            migrationBuilder.CreateTable(
                name: "post_category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_assistant = table.Column<Guid>(type: "uuid", nullable: false),
                    id_category = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    read_time = table.Column<string>(type: "text", nullable: true),
                    summary = table.Column<string>(type: "text", nullable: true),
                    thumbnail_url = table.Column<string>(type: "text", nullable: true),
                    title = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_assistant_id_assistant",
                        column: x => x.id_assistant,
                        principalTable: "assistant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_post_post_category_id_category",
                        column: x => x.id_category,
                        principalTable: "post_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_post_id_assistant",
                table: "post",
                column: "id_assistant");

            migrationBuilder.CreateIndex(
                name: "ix_post_id_category",
                table: "post",
                column: "id_category");
        }
    }
}
