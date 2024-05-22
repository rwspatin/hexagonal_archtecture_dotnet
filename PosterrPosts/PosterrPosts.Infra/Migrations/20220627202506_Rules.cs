using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosterrPosts.Infra.Migrations
{
    public partial class Rules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "POST_RELATED_ID_FK",
                table: "TB_POST",
                column: "POST_RELATED_ID",
                principalTable: "TB_POST",
                principalColumn: "Id"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
