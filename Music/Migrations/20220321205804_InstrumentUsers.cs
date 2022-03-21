using Microsoft.EntityFrameworkCore.Migrations;

namespace Music.Migrations
{
    public partial class InstrumentUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Instruments",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_UserId",
                table: "Instruments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instruments_AspNetUsers_UserId",
                table: "Instruments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instruments_AspNetUsers_UserId",
                table: "Instruments");

            migrationBuilder.DropIndex(
                name: "IX_Instruments_UserId",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Instruments");
        }
    }
}
