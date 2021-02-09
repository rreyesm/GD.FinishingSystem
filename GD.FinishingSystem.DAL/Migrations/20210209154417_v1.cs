
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "tblDefinationProcess",
            //    columns: table => new
            //    {
            //        DefinationProcessID = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        ProcessCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatorID = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
            //        LastUpdaterID = table.Column<int>(type: "INTEGER", nullable: false),
            //        LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
            //        DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
            //        DeleterID = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblDefinationProcess", x => x.DefinationProcessID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tblFloors",
            //    columns: table => new
            //    {
            //        FloorID = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        FloorName = table.Column<string>(type: "TEXT", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblFloors", x => x.FloorID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tblTestResults",
            //    columns: table => new
            //    {
            //        TestResultID = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Details = table.Column<string>(type: "TEXT", nullable: true),
            //        CanContinue = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatorID = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
            //        LastUpdaterID = table.Column<int>(type: "INTEGER", nullable: false),
            //        LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
            //        DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
            //        DeleterID = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblTestResults", x => x.TestResultID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tblUsers",
            //    columns: table => new
            //    {
            //        UserID = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        LastName = table.Column<string>(type: "TEXT", nullable: true),
            //        UserName = table.Column<string>(type: "TEXT", nullable: true),
            //        PasswordStored = table.Column<string>(type: "TEXT", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatorID = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
            //        LastUpdaterID = table.Column<int>(type: "INTEGER", nullable: false),
            //        LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
            //        DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
            //        DeleterID = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblUsers", x => x.UserID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tblMachines",
            //    columns: table => new
            //    {
            //        MachineID = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        DefinationProcessID = table.Column<int>(type: "INTEGER", nullable: false),
            //        MachineCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
            //        MachineName = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatorID = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
            //        LastUpdaterID = table.Column<int>(type: "INTEGER", nullable: false),
            //        LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
            //        DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
            //        DeleterID = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblMachines", x => x.MachineID);
            //        table.ForeignKey(
            //            name: "FK_tblMachines_tblDefinationProcess_DefinationProcessID",
            //            column: x => x.DefinationProcessID,
            //            principalTable: "tblDefinationProcess",
            //            principalColumn: "DefinationProcessID",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tblRulos",
            //    columns: table => new
            //    {
            //        RuloID = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Lote = table.Column<string>(type: "TEXT", nullable: true),
            //        Beam = table.Column<int>(type: "INTEGER", nullable: false),
            //        Loom = table.Column<int>(type: "INTEGER", nullable: false),
            //        Piece = table.Column<int>(type: "INTEGER", nullable: false),
            //        Style = table.Column<string>(type: "TEXT", nullable: true),
            //        StyleName = table.Column<string>(type: "TEXT", nullable: true),
            //        Width = table.Column<int>(type: "INTEGER", nullable: false),
            //        EntranceLength = table.Column<int>(type: "INTEGER", nullable: false),
            //        ExitLength = table.Column<int>(type: "INTEGER", nullable: false),
            //        IsWaitingAnswerFromTest = table.Column<bool>(type: "INTEGER", nullable: false),
            //        TestResultID = table.Column<int>(type: "INTEGER", nullable: true),
            //        TestResultAuthorizer = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatorID = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
            //        LastUpdaterID = table.Column<int>(type: "INTEGER", nullable: false),
            //        LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
            //        DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
            //        DeleterID = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblRulos", x => x.RuloID);
            //        table.ForeignKey(
            //            name: "FK_tblRulos_tblTestResults_TestResultID",
            //            column: x => x.TestResultID,
            //            principalTable: "tblTestResults",
            //            principalColumn: "TestResultID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tblUserRoles",
            //    columns: table => new
            //    {
            //        UserRoleID = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        UserID = table.Column<int>(type: "INTEGER", nullable: false),
            //        FormName = table.Column<string>(type: "TEXT", nullable: true),
            //        AuthorizeType = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatorID = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
            //        LastUpdaterID = table.Column<int>(type: "INTEGER", nullable: false),
            //        LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
            //        DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
            //        DeleterID = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblUserRoles", x => x.UserRoleID);
            //        table.ForeignKey(
            //            name: "FK_tblUserRoles_tblUsers_UserID",
            //            column: x => x.UserID,
            //            principalTable: "tblUsers",
            //            principalColumn: "UserID",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tblRuloProcesses",
            //    columns: table => new
            //    {
            //        RuloProcessID = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        RuloID = table.Column<int>(type: "INTEGER", nullable: false),
            //        DefinationProcessID = table.Column<int>(type: "INTEGER", nullable: false),
            //        BeginningDate = table.Column<DateTime>(type: "TEXT", nullable: false),
            //        EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
            //        FinishMeter = table.Column<decimal>(type: "TEXT", nullable: true),
            //        IsFinished = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatorID = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
            //        LastUpdaterID = table.Column<int>(type: "INTEGER", nullable: false),
            //        LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
            //        DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
            //        DeleterID = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblRuloProcesses", x => x.RuloProcessID);
            //        table.ForeignKey(
            //            name: "FK_tblRuloProcesses_tblDefinationProcess_DefinationProcessID",
            //            column: x => x.DefinationProcessID,
            //            principalTable: "tblDefinationProcess",
            //            principalColumn: "DefinationProcessID",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_tblRuloProcesses_tblRulos_RuloID",
            //            column: x => x.RuloID,
            //            principalTable: "tblRulos",
            //            principalColumn: "RuloID",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_tblMachines_DefinationProcessID",
            //    table: "tblMachines",
            //    column: "DefinationProcessID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_tblRuloProcesses_DefinationProcessID",
            //    table: "tblRuloProcesses",
            //    column: "DefinationProcessID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_tblRuloProcesses_RuloID",
            //    table: "tblRuloProcesses",
            //    column: "RuloID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_tblRulos_TestResultID",
            //    table: "tblRulos",
            //    column: "TestResultID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_tblUserRoles_UserID",
            //    table: "tblUserRoles",
            //    column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFloors");

            migrationBuilder.DropTable(
                name: "tblMachines");

            migrationBuilder.DropTable(
                name: "tblRuloProcesses");

            migrationBuilder.DropTable(
                name: "tblUserRoles");

            migrationBuilder.DropTable(
                name: "tblDefinationProcess");

            migrationBuilder.DropTable(
                name: "tblRulos");

            migrationBuilder.DropTable(
                name: "tblUsers");

            migrationBuilder.DropTable(
                name: "tblTestResults");
        }
    }
}
