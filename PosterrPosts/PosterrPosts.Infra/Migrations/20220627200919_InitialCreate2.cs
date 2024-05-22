using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosterrPosts.Infra.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostType",
                table: "TB_POST",
                newName: "POST_TYPE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "POST_TYPE",
                table: "TB_POST",
                newName: "PostType");
        }
    }
}
