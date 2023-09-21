using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "Warehouse",
            //    table: "tblRuloReports",
            //    newName: "WarehouseCode");

            //migrationBuilder.RenameColumn(
            //    name: "Origin",
            //    table: "tblRuloReports",
            //    newName: "OriginCode");

            migrationBuilder.RenameColumn(
                name: "Stop",
                table: "tblRuloMigrations",
                newName: "BeamStop");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Name",
            //    table: "tblWarehouseCategories",
            //    type: "nvarchar(max)",
            //    nullable: true,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsTestStyle",
                table: "tblRuloMigrations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.CreateTable(
            //    name: "tblOriginCategories",
            //    columns: table => new
            //    {
            //        OriginCategoryID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        OriginCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CreatorID = table.Column<int>(type: "int", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastUpdaterID = table.Column<int>(type: "int", nullable: false),
            //        LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            //        DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        DeleterID = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblOriginCategories", x => x.OriginCategoryID);
            //    });

            migrationBuilder.AddUniqueConstraint("PK_tblOriginCategories", "tblOriginCategories", "OriginCategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "tblOriginCategories");

            migrationBuilder.RenameColumn(
                name: "BeamStop",
                table: "tblRuloMigrations",
                newName: "Stop");

            migrationBuilder.DropColumn(
                name: "IsTestStyle",
                table: "tblRuloMigrations");

            //migrationBuilder.RenameColumn(
            //    name: "WarehouseCode",
            //    table: "tblRuloReports",
            //    newName: "Warehouse");

            //migrationBuilder.RenameColumn(
            //    name: "OriginCode",
            //    table: "tblRuloReports",
            //    newName: "Origin");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Name",
            //    table: "tblWarehouseCategories",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)",
            //    oldNullable: true);
        }
    }
}
