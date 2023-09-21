using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Shift",
                table: "tblRuloMigrations",
                newName: "WeavingShift");

            migrationBuilder.AddColumn<int>(
                name: "OriginID",
                table: "tblRuloMigrations",
                type: "int",
                nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "WarehouseStock",
            //    columns: table => new
            //    {
            //        Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Stock = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_tblRuloMigrations_OriginID",
                table: "tblRuloMigrations",
                column: "OriginID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblRuloMigrations_tblOriginCategories_OriginID",
                table: "tblRuloMigrations",
                column: "OriginID",
                principalTable: "tblOriginCategories",
                principalColumn: "OriginCategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblRuloMigrations_tblOriginCategories_OriginID",
                table: "tblRuloMigrations");

            //migrationBuilder.DropTable(
            //    name: "WarehouseStock");

            migrationBuilder.DropIndex(
                name: "IX_tblRuloMigrations_OriginID",
                table: "tblRuloMigrations");

            migrationBuilder.DropColumn(
                name: "OriginID",
                table: "tblRuloMigrations");

            migrationBuilder.RenameColumn(
                name: "WeavingShift",
                table: "tblRuloMigrations",
                newName: "Shift");
        }
    }
}
