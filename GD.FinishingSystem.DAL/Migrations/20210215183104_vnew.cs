using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class vnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "tblFloors",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorID",
                table: "tblFloors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "tblFloors",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeleterID",
                table: "tblFloors",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "tblFloors",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "tblFloors",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastUpdaterID",
                table: "tblFloors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "tblFloors");

            migrationBuilder.DropColumn(
                name: "CreatorID",
                table: "tblFloors");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "tblFloors");

            migrationBuilder.DropColumn(
                name: "DeleterID",
                table: "tblFloors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "tblFloors");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "tblFloors");

            migrationBuilder.DropColumn(
                name: "LastUpdaterID",
                table: "tblFloors");
        }
    }
}
