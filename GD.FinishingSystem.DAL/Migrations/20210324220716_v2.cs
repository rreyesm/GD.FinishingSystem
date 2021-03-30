using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblRuloMigrations",
                columns: table => new
                {
                    RuloMigrationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StyleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextMachine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lote = table.Column<int>(type: "int", nullable: false),
                    Beam = table.Column<int>(type: "int", nullable: false),
                    BeamLetter = table.Column<int>(type: "int", nullable: false),
                    BeamStop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loom = table.Column<int>(type: "int", nullable: false),
                    PieceNo = table.Column<int>(type: "int", nullable: false),
                    Meters = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GummedMeters = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shift = table.Column<int>(type: "int", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdaterID = table.Column<int>(type: "int", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRuloMigrations", x => x.RuloMigrationID);
                });

            migrationBuilder.CreateTable(
                name: "tblRuloReports",
                columns: table => new
                {
                    RuloID = table.Column<int>(type: "int", nullable: false),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Beam = table.Column<int>(type: "int", nullable: false),
                    BeamStop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loom = table.Column<int>(type: "int", nullable: false),
                    IsToyota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PieceCount = table.Column<int>(type: "int", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StyleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EntranceLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExitLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Shift = table.Column<int>(type: "int", nullable: false),
                    IsWaitingAnswerFromTest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanContinue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestCategoryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuloObservations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResultAuthorizer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResultObservations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FolioNumber = table.Column<int>(type: "int", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SenderID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentAuthorizerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastRuloProcess = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblRuloMigrations");

            migrationBuilder.DropTable(
                name: "tblRuloReports");
        }
    }
}
