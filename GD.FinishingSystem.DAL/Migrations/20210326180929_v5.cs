using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExcelFileName",
                table: "tblRuloMigrations",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExcelFileRow",
                table: "tblRuloMigrations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LoteLetter",
                table: "tblRuloMigrations",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PieceBetilla",
                table: "tblRuloMigrations",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ErrorMesage",
                table: "tblMigrationControls",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExcelFileName",
                table: "tblRuloMigrations");

            migrationBuilder.DropColumn(
                name: "ExcelFileRow",
                table: "tblRuloMigrations");

            migrationBuilder.DropColumn(
                name: "LoteLetter",
                table: "tblRuloMigrations");

            migrationBuilder.DropColumn(
                name: "PieceBetilla",
                table: "tblRuloMigrations");

            migrationBuilder.DropColumn(
                name: "ErrorMesage",
                table: "tblMigrationControls");
        }
    }
}
