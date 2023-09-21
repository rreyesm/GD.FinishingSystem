using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_tblOriginCategories",
            //    table: "tblOriginCategories"
            //    );

            //migrationBuilder.AlterColumn<int>(
            //    name: "OriginCategoryID",
            //    table: "tblOriginCategories",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int",
            //    oldNullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblOriginCategories_1",
                table: "tblOriginCategories",
                column: "OriginCategoryID"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
