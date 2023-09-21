using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefinitionProcessID",
                table: "tblRuloMigrations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblRuloMigrations_DefinitionProcessID",
                table: "tblRuloMigrations",
                column: "DefinitionProcessID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblRuloMigrations_tblDefinationProcess_DefinitionProcessID",
                table: "tblRuloMigrations",
                column: "DefinitionProcessID",
                principalTable: "tblDefinationProcess",
                principalColumn: "DefinationProcessID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblRuloMigrations_tblDefinationProcess_DefinitionProcessID",
                table: "tblRuloMigrations");

            migrationBuilder.DropIndex(
                name: "IX_tblRuloMigrations_DefinitionProcessID",
                table: "tblRuloMigrations");

            migrationBuilder.DropColumn(
                name: "DefinitionProcessID",
                table: "tblRuloMigrations");
        }
    }
}
