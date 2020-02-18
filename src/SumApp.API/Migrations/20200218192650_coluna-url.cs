using Microsoft.EntityFrameworkCore.Migrations;

namespace SumApp.API.Migrations
{
    public partial class colunaurl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Teams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Teams");
        }
    }
}
