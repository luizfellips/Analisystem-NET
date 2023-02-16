using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Analisystem.Migrations
{
    public partial class AlterFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserModelId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserModelId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserModelId",
                table: "Products",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserModelId",
                table: "Products",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
