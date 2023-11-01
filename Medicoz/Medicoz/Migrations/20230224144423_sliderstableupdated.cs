using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicoz.Migrations
{
    public partial class sliderstableupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Button1",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "Button2",
                table: "Sliders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Button1",
                table: "Sliders",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Button2",
                table: "Sliders",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }
    }
}
