using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "StockSymbol",
            //    table: "WarehouseStock",
            //    type: "nvarchar(max)",
            //    nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MainOrigin",
                table: "tblRulos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "CuttingFinishDate",
            //    table: "tblRuloReports",
            //    type: "datetime2",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "CuttingStartDate",
            //    table: "tblRuloReports",
            //    type: "datetime2",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "InspectionFinishDate",
            //    table: "tblRuloReports",
            //    type: "datetime2",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "InspectionStartDate",
            //    table: "tblRuloReports",
            //    type: "datetime2",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "MainOrigin",
            //    table: "tblRuloReports",
            //    type: "nvarchar(max)",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "StockSymbol",
            //    table: "WarehouseStock");

            migrationBuilder.DropColumn(
                name: "MainOrigin",
                table: "tblRulos");

            //migrationBuilder.DropColumn(
            //    name: "CuttingFinishDate",
            //    table: "tblRuloReports");

            //migrationBuilder.DropColumn(
            //    name: "CuttingStartDate",
            //    table: "tblRuloReports");

            //migrationBuilder.DropColumn(
            //    name: "InspectionFinishDate",
            //    table: "tblRuloReports");

            //migrationBuilder.DropColumn(
            //    name: "InspectionStartDate",
            //    table: "tblRuloReports");

            //migrationBuilder.DropColumn(
            //    name: "MainOrigin",
            //    table: "tblRuloReports");
        }
    }
}
