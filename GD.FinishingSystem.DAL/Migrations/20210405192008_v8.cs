using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tblSystemPrinters",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "tblSystemPrinters",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "MachineID",
                table: "tblSystemPrinters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PCIP",
                table: "tblSystemPrinters",
                type: "nvarchar(32)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tblSystemPrinters_MachineID",
                table: "tblSystemPrinters",
                column: "MachineID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblSystemPrinters_tblMachines_MachineID",
                table: "tblSystemPrinters",
                column: "MachineID",
                principalTable: "tblMachines",
                principalColumn: "MachineID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblSystemPrinters_tblMachines_MachineID",
                table: "tblSystemPrinters");

            migrationBuilder.DropIndex(
                name: "IX_tblSystemPrinters_MachineID",
                table: "tblSystemPrinters");

            migrationBuilder.DropColumn(
                name: "MachineID",
                table: "tblSystemPrinters");

            migrationBuilder.DropColumn(
                name: "PCIP",
                table: "tblSystemPrinters");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tblSystemPrinters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "tblSystemPrinters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
