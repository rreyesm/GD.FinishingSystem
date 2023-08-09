using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Shrinkage",
                table: "tblRulos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            //migrationBuilder.CreateTable(
            //    name: "tblCustomReports",
            //    columns: table => new
            //    {
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Shift = table.Column<int>(type: "int", nullable: false),
            //        Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        StyleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        FinishMeterRama = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        FinishMeterRP = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        ExitLength = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tblRuloBatches",
            //    columns: table => new
            //    {
            //        RuloID = table.Column<int>(type: "int", nullable: false),
            //        BatchNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Inspectionlength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        CuttingLenght = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tblRuloReports",
            //    columns: table => new
            //    {
            //        RuloID = table.Column<int>(type: "int", nullable: false),
            //        Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Beam = table.Column<int>(type: "int", nullable: false),
            //        BeamStop = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Loom = table.Column<int>(type: "int", nullable: false),
            //        IsToyota = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PieceCount = table.Column<int>(type: "int", nullable: false),
            //        Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        StyleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        WeavingLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        EntranceLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        ExitLengthRama = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        ExitLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        ExitLengthMinusSamples = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        Shrinkage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        InspectionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        InspectionCuttingLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        Shift = table.Column<int>(type: "int", nullable: false),
            //        IsWaitingAnswerFromTest = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CanContinue = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TestCategoryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DateTestResult = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        BatchNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        RuloObservations = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TestResultAuthorizer = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TestResultObservations = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        FolioNumber = table.Column<int>(type: "int", nullable: false),
            //        SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        Sender = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SentAuthorizer = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Creator = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Machine = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "tblCustomReports");

            //migrationBuilder.DropTable(
            //    name: "tblRuloBatches");

            //migrationBuilder.DropTable(
            //    name: "tblRuloReports");

            migrationBuilder.DropColumn(
                name: "Shrinkage",
                table: "tblRulos");
        }
    }
}
