using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicoz.Migrations
{
    public partial class SettingsTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Settings",
                type: "nvarchar(101)",
                maxLength: 101,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsImage",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "IsImage",
                table: "Settings");
        }
    }
}
