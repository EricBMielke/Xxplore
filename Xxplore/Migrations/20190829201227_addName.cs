using Microsoft.EntityFrameworkCore.Migrations;

namespace Xxplore.Migrations
{
    public partial class addName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WishList1Name",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WishList2Name",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WishList3Name",
                table: "UserProfile",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WishList1Name",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "WishList2Name",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "WishList3Name",
                table: "UserProfile");
        }
    }
}
