using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class vnew2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "tblTestCategories",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorID",
                table: "tblTestCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "tblTestCategories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeleterID",
                table: "tblTestCategories",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "tblTestCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "tblTestCategories",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastUpdaterID",
                table: "tblTestCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "tblOriginCategories",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorID",
                table: "tblOriginCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "tblOriginCategories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeleterID",
                table: "tblOriginCategories",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "tblOriginCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "tblOriginCategories",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastUpdaterID",
                table: "tblOriginCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "tblTestCategories");

            migrationBuilder.DropColumn(
                name: "CreatorID",
                table: "tblTestCategories");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "tblTestCategories");

            migrationBuilder.DropColumn(
                name: "DeleterID",
                table: "tblTestCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "tblTestCategories");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "tblTestCategories");

            migrationBuilder.DropColumn(
                name: "LastUpdaterID",
                table: "tblTestCategories");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "tblOriginCategories");

            migrationBuilder.DropColumn(
                name: "CreatorID",
                table: "tblOriginCategories");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "tblOriginCategories");

            migrationBuilder.DropColumn(
                name: "DeleterID",
                table: "tblOriginCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "tblOriginCategories");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "tblOriginCategories");

            migrationBuilder.DropColumn(
                name: "LastUpdaterID",
                table: "tblOriginCategories");
        }
    }
}
