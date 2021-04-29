using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SystemPrinterID",
                table: "tblPeriods",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblPeriods_SystemPrinterID",
                table: "tblPeriods",
                column: "SystemPrinterID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblPeriods_tblSystemPrinters_SystemPrinterID",
                table: "tblPeriods",
                column: "SystemPrinterID",
                principalTable: "tblSystemPrinters",
                principalColumn: "SystemPrinterID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblPeriods_tblSystemPrinters_SystemPrinterID",
                table: "tblPeriods");

            migrationBuilder.DropIndex(
                name: "IX_tblPeriods_SystemPrinterID",
                table: "tblPeriods");

            migrationBuilder.DropColumn(
                name: "SystemPrinterID",
                table: "tblPeriods");
        }
    }
}
