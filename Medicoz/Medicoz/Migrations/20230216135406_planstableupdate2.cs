using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicoz.Migrations
{
    public partial class planstableupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_PlanCategories_PlanCategoryId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Plans");

            migrationBuilder.AlterColumn<int>(
                name: "PlanCategoryId",
                table: "Plans",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_PlanCategories_PlanCategoryId",
                table: "Plans",
                column: "PlanCategoryId",
                principalTable: "PlanCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_PlanCategories_PlanCategoryId",
                table: "Plans");

            migrationBuilder.AlterColumn<int>(
                name: "PlanCategoryId",
                table: "Plans",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Plans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_PlanCategories_PlanCategoryId",
                table: "Plans",
                column: "PlanCategoryId",
                principalTable: "PlanCategories",
                principalColumn: "Id");
        }
    }
}
