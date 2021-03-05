using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class vnew3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblSampleDetails",
                columns: table => new
                {
                    SampleDetailID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SampleID = table.Column<int>(type: "INTEGER", nullable: false),
                    RuloID = table.Column<int>(type: "INTEGER", nullable: false),
                    RuloProcessID = table.Column<int>(type: "INTEGER", nullable: false),
                    Meter = table.Column<float>(type: "REAL", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Details = table.Column<string>(type: "TEXT", nullable: true),
                    CreatorID = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdaterID = table.Column<int>(type: "INTEGER", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeleterID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSampleDetails", x => x.SampleDetailID);
                });

            migrationBuilder.CreateTable(
                name: "tblSamples",
                columns: table => new
                {
                    SampleID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RuloID = table.Column<int>(type: "INTEGER", nullable: false),
                    RuloProcessID = table.Column<int>(type: "INTEGER", nullable: false),
                    Meter = table.Column<float>(type: "REAL", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorID = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdaterID = table.Column<int>(type: "INTEGER", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeleterID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSamples", x => x.SampleID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblSampleDetails");

            migrationBuilder.DropTable(
                name: "tblSamples");
        }
    }
}
