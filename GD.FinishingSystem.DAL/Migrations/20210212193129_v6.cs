using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "TestID",
                table: "tblTestCategories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<short>(
                name: "OriginID",
                table: "tblOriginCategories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<short>(
                name: "FloorID",
                table: "tblFloors",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "TestID",
                table: "tblTestCategories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<byte>(
                name: "OriginID",
                table: "tblOriginCategories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<byte>(
                name: "FloorID",
                table: "tblFloors",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }
    }
}
