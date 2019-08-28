using Microsoft.EntityFrameworkCore.Migrations;

namespace Xxplore.Migrations
{
    public partial class ChangeUserProfileModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hasVisited",
                table: "CountriesVisited");

            migrationBuilder.DropColumn(
                name: "hasntVisited",
                table: "CountriesVisited");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "CountriesVisited",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "CountriesVisited");

            migrationBuilder.AddColumn<bool>(
                name: "hasVisited",
                table: "CountriesVisited",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "hasntVisited",
                table: "CountriesVisited",
                nullable: false,
                defaultValue: false);
        }
    }
}
