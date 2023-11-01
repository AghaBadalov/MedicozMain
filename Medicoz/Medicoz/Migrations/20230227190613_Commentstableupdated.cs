using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicoz.Migrations
{
    public partial class Commentstableupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BlogComments",
                type: "nvarchar(55)",
                maxLength: 55,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "BlogComments");
        }
    }
}
