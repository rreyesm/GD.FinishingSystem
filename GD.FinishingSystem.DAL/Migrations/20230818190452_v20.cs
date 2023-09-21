using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsToyota",
                table: "tblRuloMigrations",
                newName: "IsToyotaText");

            migrationBuilder.AddColumn<bool>(
                name: "IsToyotaMigration",
                table: "tblRuloMigrations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsToyotaMigration",
                table: "tblRuloMigrations");

            migrationBuilder.RenameColumn(
                name: "IsToyotaText",
                table: "tblRuloMigrations",
                newName: "IsToyota");
        }
    }
}
