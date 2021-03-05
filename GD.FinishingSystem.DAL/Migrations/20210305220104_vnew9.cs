using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class vnew9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblSampleDetails");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "tblSamples");

            migrationBuilder.DropColumn(
                name: "RuloID",
                table: "tblSamples");

            migrationBuilder.AddColumn<string>(
                name: "CutterPerson",
                table: "tblSamples",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "tblSamples",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CutterPerson",
                table: "tblSamples");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "tblSamples");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "tblSamples",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RuloID",
                table: "tblSamples",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tblSampleDetails",
                columns: table => new
                {
                    SampleDetailID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorID = table.Column<int>(type: "INTEGER", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeleterID = table.Column<int>(type: "INTEGER", nullable: true),
                    Details = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdaterID = table.Column<int>(type: "INTEGER", nullable: false),
                    Meter = table.Column<decimal>(type: "TEXT", nullable: false),
                    RuloID = table.Column<int>(type: "INTEGER", nullable: false),
                    RuloProcessID = table.Column<int>(type: "INTEGER", nullable: false),
                    SampleID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSampleDetails", x => x.SampleDetailID);
                });
        }
    }
}
