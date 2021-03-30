using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeamLetter",
                table: "tblRuloMigrations");

            migrationBuilder.CreateTable(
                name: "tblMigrationControls",
                columns: table => new
                {
                    MigrationControlId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcelFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastMigratedRowOfExcelFile = table.Column<int>(type: "int", nullable: false),
                    FileRowsTotal = table.Column<int>(type: "int", nullable: false),
                    RowsTotalMigrated = table.Column<int>(type: "int", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMigrationControls", x => x.MigrationControlId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblMigrationControls");

            migrationBuilder.AddColumn<int>(
                name: "BeamLetter",
                table: "tblRuloMigrations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
