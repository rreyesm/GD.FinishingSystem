using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoteLetter",
                table: "tblRuloMigrations",
                newName: "Stop");

            migrationBuilder.RenameColumn(
                name: "BeamStop",
                table: "tblRuloMigrations",
                newName: "IsToyota");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stop",
                table: "tblRuloMigrations",
                newName: "LoteLetter");

            migrationBuilder.RenameColumn(
                name: "IsToyota",
                table: "tblRuloMigrations",
                newName: "BeamStop");
        }
    }
}
