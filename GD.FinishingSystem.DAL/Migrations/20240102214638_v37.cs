using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v37 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishingMachine",
                table: "tblReprocesses");

            migrationBuilder.AddColumn<int>(
                name: "DefinitionProcessID",
                table: "tblReprocesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PackingListNo",
                table: "tblReprocesses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RuloID",
                table: "tblReprocesses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackingListNo",
                table: "tblPackingList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PackingListType",
                table: "tblPackingList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblReprocesses_DefinitionProcessID",
                table: "tblReprocesses",
                column: "DefinitionProcessID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblReprocesses_tblDefinationProcess_DefinitionProcessID",
                table: "tblReprocesses",
                column: "DefinitionProcessID",
                principalTable: "tblDefinationProcess",
                principalColumn: "DefinationProcessID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblReprocesses_tblDefinationProcess_DefinitionProcessID",
                table: "tblReprocesses");

            migrationBuilder.DropIndex(
                name: "IX_tblReprocesses_DefinitionProcessID",
                table: "tblReprocesses");

            migrationBuilder.DropColumn(
                name: "DefinitionProcessID",
                table: "tblReprocesses");

            migrationBuilder.DropColumn(
                name: "PackingListNo",
                table: "tblReprocesses");

            migrationBuilder.DropColumn(
                name: "RuloID",
                table: "tblReprocesses");

            migrationBuilder.DropColumn(
                name: "PackingListNo",
                table: "tblPackingList");

            migrationBuilder.DropColumn(
                name: "PackingListType",
                table: "tblPackingList");

            migrationBuilder.AddColumn<string>(
                name: "FinishingMachine",
                table: "tblReprocesses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
