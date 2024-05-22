using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PosterrPosts.Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_USER",
                columns: table => new
                {
                    ID_USER = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    USER_NAME = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER", x => x.ID_USER);
                });

            migrationBuilder.CreateTable(
                name: "TB_POST",
                columns: table => new
                {
                    ID_POST = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    POST_TEXT = table.Column<string>(type: "character varying(777)", maxLength: 777, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    USER_ID = table.Column<int>(type: "integer", nullable: false),
                    POST_RELATED_ID = table.Column<int>(type: "integer", nullable: true),
                    PostType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_POST", x => x.ID_POST);
                    table.ForeignKey(
                        name: "FK_TB_POST_TB_POST_POST_RELATED_ID",
                        column: x => x.POST_RELATED_ID,
                        principalTable: "TB_POST",
                        principalColumn: "ID_POST");
                    table.ForeignKey(
                        name: "FK_TB_POST_TB_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "TB_USER",
                        principalColumn: "ID_USER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_POST_POST_RELATED_ID",
                table: "TB_POST",
                column: "POST_RELATED_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_POST_USER_ID",
                table: "TB_POST",
                column: "USER_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_POST");

            migrationBuilder.DropTable(
                name: "TB_USER");
        }
    }
}
