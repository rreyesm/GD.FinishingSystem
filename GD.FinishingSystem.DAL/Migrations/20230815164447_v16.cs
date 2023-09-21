using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "StockID",
            //    table: "tblRulos");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseCategoryID",
                table: "tblRulos",
                type: "int",
                nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Warehouse",
            //    table: "tblRuloReports",
            //    type: "nvarchar(max)",
            //    nullable: true);

            migrationBuilder.CreateTable(
                name: "tblWarehouseCategories",
                columns: table => new
                {
                    WarehouseCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_tblWarehouseCategories", x => x.WarehouseCategoryID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblRulos_WarehouseCategoryID",
                table: "tblRulos",
                column: "WarehouseCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblRulos_tblWarehouseCategories_WarehouseCategoryID",
                table: "tblRulos",
                column: "WarehouseCategoryID",
                principalTable: "tblWarehouseCategories",
                principalColumn: "WarehouseCategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblRulos_tblWarehouseCategories_WarehouseCategoryID",
                table: "tblRulos");

            migrationBuilder.DropTable(
                name: "tblWarehouseCategories");

            migrationBuilder.DropIndex(
                name: "IX_tblRulos_WarehouseCategoryID",
                table: "tblRulos");

            migrationBuilder.DropColumn(
                name: "WarehouseCategoryID",
                table: "tblRulos");

            //migrationBuilder.DropColumn(
            //    name: "Warehouse",
            //    table: "tblRuloReports");

            //migrationBuilder.AddColumn<int>(
            //    name: "StockID",
            //    table: "tblRulos",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);
        }
    }
}
