using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTestStyle",
                table: "tblRulos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ExitLengthRama",
                table: "tblRuloReports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "tblCustomReports",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Shift = table.Column<int>(type: "int", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    StyleName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Lote = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FinishMeterRama = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinishMeterRP = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExitLength = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCustomReports");

            migrationBuilder.DropColumn(
                name: "IsTestStyle",
                table: "tblRulos");

            migrationBuilder.DropColumn(
                name: "ExitLengthRama",
                table: "tblRuloReports");
        }
    }
}
