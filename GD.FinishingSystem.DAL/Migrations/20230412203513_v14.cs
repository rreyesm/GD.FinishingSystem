using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCustomReports");

            migrationBuilder.DropTable(
                name: "tblRuloReports");

            migrationBuilder.AddColumn<decimal>(
                name: "WeavingLength",
                table: "tblRulos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeavingLength",
                table: "tblRulos");

            migrationBuilder.CreateTable(
                name: "TblCustomReport",
                columns: table => new
                {
                    ExitLength = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinishMeterRP = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinishMeterRama = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Lote = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Shift = table.Column<int>(type: "int", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    StyleName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tblRuloReports",
                columns: table => new
                {
                    Beam = table.Column<int>(type: "int", nullable: false),
                    BeamStop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanContinue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntranceLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExitLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExitLengthRama = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FolioNumber = table.Column<int>(type: "int", nullable: false),
                    IsToyota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsWaitingAnswerFromTest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastRuloProcess = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loom = table.Column<int>(type: "int", nullable: false),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PieceCount = table.Column<int>(type: "int", nullable: false),
                    RuloID = table.Column<int>(type: "int", nullable: false),
                    RuloObservations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentAuthorizerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Shift = table.Column<int>(type: "int", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StyleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestCategoryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResultAuthorizer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResultObservations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });
        }
    }
}
