using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicoz.Migrations
{
    public partial class updateplanstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanCategories_Plans_PlanId",
                table: "PlanCategories");

            migrationBuilder.DropIndex(
                name: "IX_PlanCategories_PlanId",
                table: "PlanCategories");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "PlanCategories");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Plans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PlanCategoryId",
                table: "Plans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PlanCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Plans_PlanCategoryId",
                table: "Plans",
                column: "PlanCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_PlanCategories_PlanCategoryId",
                table: "Plans",
                column: "PlanCategoryId",
                principalTable: "PlanCategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_PlanCategories_PlanCategoryId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_PlanCategoryId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "PlanCategoryId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PlanCategories");

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "PlanCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanCategories_PlanId",
                table: "PlanCategories",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanCategories_Plans_PlanId",
                table: "PlanCategories",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");
        }
    }
}
