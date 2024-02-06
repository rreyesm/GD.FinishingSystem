using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v39 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "PackingListID",
            //    table: "VMRuloMigrationReport",
            //    type: "int",
            //    nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AccountingDate",
                table: "tblRuloMigrations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AccountingDate",
                table: "tblReprocesses",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "PackingListID",
            //    table: "VMRuloMigrationReport");

            migrationBuilder.DropColumn(
                name: "AccountingDate",
                table: "tblRuloMigrations");

            migrationBuilder.DropColumn(
                name: "AccountingDate",
                table: "tblReprocesses");
        }
    }
}
