using Microsoft.EntityFrameworkCore.Migrations;

namespace VoterListApp.Migrations.ApplicationDb
{
    public partial class ImageAddedInModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Citizens",
                type: "nvarchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Citizens");
        }
    }
}
