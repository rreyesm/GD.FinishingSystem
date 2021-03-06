﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class vnew4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SampleID",
                table: "tblRuloProcesses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SampleID1",
                table: "tblRuloProcesses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblRuloProcesses_SampleID1",
                table: "tblRuloProcesses",
                column: "SampleID1");

            migrationBuilder.AddForeignKey(
                name: "FK_tblRuloProcesses_tblSamples_SampleID1",
                table: "tblRuloProcesses",
                column: "SampleID1",
                principalTable: "tblSamples",
                principalColumn: "SampleID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblRuloProcesses_tblSamples_SampleID1",
                table: "tblRuloProcesses");

            migrationBuilder.DropIndex(
                name: "IX_tblRuloProcesses_SampleID1",
                table: "tblRuloProcesses");

            migrationBuilder.DropColumn(
                name: "SampleID",
                table: "tblRuloProcesses");

            migrationBuilder.DropColumn(
                name: "SampleID1",
                table: "tblRuloProcesses");
        }
    }
}
