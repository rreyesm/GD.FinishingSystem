using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "tblRuloMigrations");

            migrationBuilder.AddColumn<int>(
                name: "MigrationCategoryID",
                table: "tblRuloMigrations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RuloID",
                table: "tblRuloMigrations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblMigrationCategories",
                columns: table => new
                {
                    MigrationCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdaterID = table.Column<int>(type: "int", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMigrationCategories", x => x.MigrationCategoryID);
                });

 
            migrationBuilder.CreateIndex(
                name: "IX_tblRuloMigrations_MigrationCategoryID",
                table: "tblRuloMigrations",
                column: "MigrationCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRuloMigrations_RuloID",
                table: "tblRuloMigrations",
                column: "RuloID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblRuloMigrations_tblMigrationCategories_MigrationCategoryID",
                table: "tblRuloMigrations",
                column: "MigrationCategoryID",
                principalTable: "tblMigrationCategories",
                principalColumn: "MigrationCategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblRuloMigrations_tblRulos_RuloID",
                table: "tblRuloMigrations",
                column: "RuloID",
                principalTable: "tblRulos",
                principalColumn: "RuloID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblRuloMigrations_tblMigrationCategories_MigrationCategoryID",
                table: "tblRuloMigrations");

            migrationBuilder.DropForeignKey(
                name: "FK_tblRuloMigrations_tblRulos_RuloID",
                table: "tblRuloMigrations");

            migrationBuilder.DropTable(
                name: "tblMigrationCategories");

            migrationBuilder.DropIndex(
                name: "IX_tblRuloMigrations_MigrationCategoryID",
                table: "tblRuloMigrations");

            migrationBuilder.DropIndex(
                name: "IX_tblRuloMigrations_RuloID",
                table: "tblRuloMigrations");

            migrationBuilder.DropColumn(
                name: "MigrationCategoryID",
                table: "tblRuloMigrations");

            migrationBuilder.DropColumn(
                name: "RuloID",
                table: "tblRuloMigrations");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "tblRuloMigrations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
