using Microsoft.EntityFrameworkCore.Migrations;

namespace Xxplore.Migrations
{
    public partial class UpdateAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "CountriesVisited",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "CountriesVisited",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
