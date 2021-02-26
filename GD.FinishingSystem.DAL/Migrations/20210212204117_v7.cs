using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TestID",
                table: "tblTestCategories",
                newName: "TestCategoryID");

            migrationBuilder.RenameColumn(
                name: "OriginID",
                table: "tblOriginCategories",
                newName: "OriginCategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TestCategoryID",
                table: "tblTestCategories",
                newName: "TestID");

            migrationBuilder.RenameColumn(
                name: "OriginCategoryID",
                table: "tblOriginCategories",
                newName: "OriginID");
        }
    }
}
