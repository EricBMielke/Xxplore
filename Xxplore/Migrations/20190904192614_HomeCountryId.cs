using Microsoft.EntityFrameworkCore.Migrations;

namespace Xxplore.Migrations
{
    public partial class HomeCountryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HomeCountryId",
                table: "UserProfile",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeCountryId",
                table: "UserProfile");
        }
    }
}
