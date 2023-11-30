using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "IsWeavingUser",
            //    table: "tblUsers",
            //    newName: "AreaID");

            migrationBuilder.AlterColumn<int>(
                name: "AreaID",
                table: "tblUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.RenameColumn(
                name: "GummedMeters",
                table: "tblRuloMigrations",
                newName: "SizingMeters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "AreaID",
            //    table: "tblUsers",
            //    newName: "IsWeavingUser");

            migrationBuilder.AlterColumn<bool>(
                name: "AreaID",
                table: "tblUsers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.RenameColumn(
                name: "SizingMeters",
                table: "tblRuloMigrations",
                newName: "GummedMeters");
        }
    }
}
