using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Origin",
                table: "tblRulos",
                newName: "OriginID");

            migrationBuilder.AddColumn<byte>(
                name: "TestID",
                table: "tblTestResults",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AlterColumn<string>(
                name: "FloorName",
                table: "tblFloors",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "FloorID",
                table: "tblFloors",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "tblOriginCategories",
                columns: table => new
                {
                    OriginID = table.Column<byte>(type: "INTEGER", nullable: false),
                    OriginCode = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOriginCategories", x => x.OriginID);
                });

            migrationBuilder.CreateTable(
                name: "tblTestCategories",
                columns: table => new
                {
                    TestID = table.Column<byte>(type: "INTEGER", nullable: false),
                    TestCode = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTestCategories", x => x.TestID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblOriginCategories");

            migrationBuilder.DropTable(
                name: "tblTestCategories");

            migrationBuilder.DropColumn(
                name: "TestID",
                table: "tblTestResults");

            migrationBuilder.RenameColumn(
                name: "OriginID",
                table: "tblRulos",
                newName: "Origin");

            migrationBuilder.AlterColumn<string>(
                name: "FloorName",
                table: "tblFloors",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "FloorID",
                table: "tblFloors",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);
        }
    }
}
