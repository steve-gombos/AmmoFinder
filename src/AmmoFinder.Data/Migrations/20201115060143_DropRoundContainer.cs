using Microsoft.EntityFrameworkCore.Migrations;

namespace AmmoFinder.Data.Migrations
{
    public partial class DropRoundContainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoundContainer",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoundContainer",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
