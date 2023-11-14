using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainOrigin",
                table: "tblRulos",
                newName: "MainOriginID");

            //migrationBuilder.RenameColumn(
            //    name: "MainOrigin",
            //    table: "tblRuloReports",
            //    newName: "MainOriginCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainOriginID",
                table: "tblRulos",
                newName: "MainOrigin");

            //migrationBuilder.RenameColumn(
            //    name: "MainOriginCode",
            //    table: "tblRuloReports",
            //    newName: "MainOrigin");
        }
    }
}
