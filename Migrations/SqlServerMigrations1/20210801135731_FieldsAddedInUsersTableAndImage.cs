using Microsoft.EntityFrameworkCore.Migrations;

namespace VoterListApp.Migrations.SqlServerMigrations1
{
    public partial class FieldsAddedInUsersTableAndImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserImageName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserImageName",
                table: "AspNetUsers");
        }
    }
}
