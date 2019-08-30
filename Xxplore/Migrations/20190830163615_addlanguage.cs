using Microsoft.EntityFrameworkCore.Migrations;

namespace Xxplore.Migrations
{
    public partial class addlanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NativeLanguage",
                table: "UserProfile",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NativeLanguage",
                table: "UserProfile");
        }
    }
}
