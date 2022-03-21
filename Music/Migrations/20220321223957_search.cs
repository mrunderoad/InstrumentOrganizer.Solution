using Microsoft.EntityFrameworkCore.Migrations;

namespace Music.Migrations
{
    public partial class search : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Search",
                table: "Items",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Search",
                table: "Items");
        }
    }
}
