using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class vnew5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Meter",
                table: "tblSamples",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<decimal>(
                name: "Meter",
                table: "tblSampleDetails",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<decimal>(
                name: "Width",
                table: "tblRulos",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "BeamStop",
                table: "tblRulos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoomLetter",
                table: "tblRulos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PieceLetter",
                table: "tblRulos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Turn",
                table: "tblRulos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeamStop",
                table: "tblRulos");

            migrationBuilder.DropColumn(
                name: "LoomLetter",
                table: "tblRulos");

            migrationBuilder.DropColumn(
                name: "PieceLetter",
                table: "tblRulos");

            migrationBuilder.DropColumn(
                name: "Turn",
                table: "tblRulos");

            migrationBuilder.AlterColumn<float>(
                name: "Meter",
                table: "tblSamples",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<float>(
                name: "Meter",
                table: "tblSampleDetails",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Width",
                table: "tblRulos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }
    }
}
