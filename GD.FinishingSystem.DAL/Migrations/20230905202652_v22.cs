using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblRuloMigrations_tblOriginCategories_OriginID",
                table: "tblRuloMigrations");

            migrationBuilder.AlterColumn<int>(
                name: "OriginID",
                table: "tblRuloMigrations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseCategoryID",
                table: "tblRuloMigrations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblRuloMigrations_WarehouseCategoryID",
                table: "tblRuloMigrations",
                column: "WarehouseCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblRuloMigrations_tblOriginCategories_OriginID",
                table: "tblRuloMigrations",
                column: "OriginID",
                principalTable: "tblOriginCategories",
                principalColumn: "OriginCategoryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblRuloMigrations_tblWarehouseCategories_WarehouseCategoryID",
                table: "tblRuloMigrations",
                column: "WarehouseCategoryID",
                principalTable: "tblWarehouseCategories",
                principalColumn: "WarehouseCategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblRuloMigrations_tblOriginCategories_OriginID",
                table: "tblRuloMigrations");

            migrationBuilder.DropForeignKey(
                name: "FK_tblRuloMigrations_tblWarehouseCategories_WarehouseCategoryID",
                table: "tblRuloMigrations");

            migrationBuilder.DropIndex(
                name: "IX_tblRuloMigrations_WarehouseCategoryID",
                table: "tblRuloMigrations");

            migrationBuilder.DropColumn(
                name: "WarehouseCategoryID",
                table: "tblRuloMigrations");

            migrationBuilder.AlterColumn<int>(
                name: "OriginID",
                table: "tblRuloMigrations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tblRuloMigrations_tblOriginCategories_OriginID",
                table: "tblRuloMigrations",
                column: "OriginID",
                principalTable: "tblOriginCategories",
                principalColumn: "OriginCategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
