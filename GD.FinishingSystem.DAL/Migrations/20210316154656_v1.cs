using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblDefinationProcess",
                columns: table => new
                {
                    DefinationProcessID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMustSample = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_tblDefinationProcess", x => x.DefinationProcessID);
                });

            migrationBuilder.CreateTable(
                name: "tblFloors",
                columns: table => new
                {
                    FloorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_tblFloors", x => x.FloorID);
                });

            migrationBuilder.CreateTable(
                name: "tblPeriods",
                columns: table => new
                {
                    PeriodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastPeriod = table.Column<long>(type: "bigint", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_tblPeriods", x => x.PeriodID);
                });

            migrationBuilder.CreateTable(
                name: "tblShifts",
                columns: table => new
                {
                    ShiftID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    StartHour = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndHour = table.Column<TimeSpan>(type: "time", nullable: false),
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
                    table.PrimaryKey("PK_tblShifts", x => x.ShiftID);
                });

            migrationBuilder.CreateTable(
                name: "tblTestCategories",
                columns: table => new
                {
                    TestCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_tblTestCategories", x => x.TestCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "tblUsers",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordStored = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_tblUsers", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "tblMachines",
                columns: table => new
                {
                    MachineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefinationProcessID = table.Column<int>(type: "int", nullable: false),
                    MachineCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FloorID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_tblMachines", x => x.MachineID);
                    table.ForeignKey(
                        name: "FK_tblMachines_tblDefinationProcess_DefinationProcessID",
                        column: x => x.DefinationProcessID,
                        principalTable: "tblDefinationProcess",
                        principalColumn: "DefinationProcessID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblTestResults",
                columns: table => new
                {
                    TestResultID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<bool>(type: "bit", nullable: false),
                    CanContinue = table.Column<bool>(type: "bit", nullable: false),
                    TestCategoryID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_tblTestResults", x => x.TestResultID);
                    table.ForeignKey(
                        name: "FK_tblTestResults_tblTestCategories_TestCategoryID",
                        column: x => x.TestCategoryID,
                        principalTable: "tblTestCategories",
                        principalColumn: "TestCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblUserRoles",
                columns: table => new
                {
                    UserRoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    FormName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorizeType = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_tblUserRoles", x => x.UserRoleID);
                    table.ForeignKey(
                        name: "FK_tblUserRoles_tblUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "tblUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblRulos",
                columns: table => new
                {
                    RuloID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Beam = table.Column<int>(type: "int", nullable: false),
                    BeamStop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loom = table.Column<int>(type: "int", nullable: false),
                    IsToyota = table.Column<bool>(type: "bit", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StyleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EntranceLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExitLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Shift = table.Column<int>(type: "int", nullable: false),
                    IsWaitingAnswerFromTest = table.Column<bool>(type: "bit", nullable: false),
                    TestResultID = table.Column<int>(type: "int", nullable: true),
                    TestResultAuthorizer = table.Column<int>(type: "int", nullable: true),
                    OriginID = table.Column<int>(type: "int", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FolioNumber = table.Column<int>(type: "int", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SenderID = table.Column<int>(type: "int", nullable: true),
                    SentAuthorizerID = table.Column<int>(type: "int", nullable: true),
                    PieceCount = table.Column<int>(type: "int", nullable: false),
                    PeriodID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_tblRulos", x => x.RuloID);
                    table.ForeignKey(
                        name: "FK_tblRulos_tblPeriods_PeriodID",
                        column: x => x.PeriodID,
                        principalTable: "tblPeriods",
                        principalColumn: "PeriodID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblRulos_tblTestResults_TestResultID",
                        column: x => x.TestResultID,
                        principalTable: "tblTestResults",
                        principalColumn: "TestResultID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblRulos_tblUsers_SenderID",
                        column: x => x.SenderID,
                        principalTable: "tblUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblRulos_tblUsers_SentAuthorizerID",
                        column: x => x.SentAuthorizerID,
                        principalTable: "tblUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPieces",
                columns: table => new
                {
                    PieceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuloID = table.Column<int>(type: "int", nullable: false),
                    PieceNo = table.Column<int>(type: "int", nullable: false),
                    Meter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_tblPieces", x => x.PieceID);
                    table.ForeignKey(
                        name: "FK_tblPieces_tblRulos_RuloID",
                        column: x => x.RuloID,
                        principalTable: "tblRulos",
                        principalColumn: "RuloID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblRuloProcesses",
                columns: table => new
                {
                    RuloProcessID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuloID = table.Column<int>(type: "int", nullable: false),
                    DefinationProcessID = table.Column<int>(type: "int", nullable: false),
                    BeginningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_tblRuloProcesses", x => x.RuloProcessID);
                    table.ForeignKey(
                        name: "FK_tblRuloProcesses_tblDefinationProcess_DefinationProcessID",
                        column: x => x.DefinationProcessID,
                        principalTable: "tblDefinationProcess",
                        principalColumn: "DefinationProcessID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblRuloProcesses_tblRulos_RuloID",
                        column: x => x.RuloID,
                        principalTable: "tblRulos",
                        principalColumn: "RuloID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSamples",
                columns: table => new
                {
                    SampleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuloProcessID = table.Column<int>(type: "int", nullable: false),
                    Meter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CutterID = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_tblSamples", x => x.SampleID);
                    table.ForeignKey(
                        name: "FK_tblSamples_tblRuloProcesses_RuloProcessID",
                        column: x => x.RuloProcessID,
                        principalTable: "tblRuloProcesses",
                        principalColumn: "RuloProcessID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSamples_tblUsers_CutterID",
                        column: x => x.CutterID,
                        principalTable: "tblUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblMachines_DefinationProcessID",
                table: "tblMachines",
                column: "DefinationProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_tblPieces_RuloID",
                table: "tblPieces",
                column: "RuloID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRuloProcesses_DefinationProcessID",
                table: "tblRuloProcesses",
                column: "DefinationProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRuloProcesses_RuloID",
                table: "tblRuloProcesses",
                column: "RuloID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRulos_PeriodID",
                table: "tblRulos",
                column: "PeriodID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRulos_SenderID",
                table: "tblRulos",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRulos_SentAuthorizerID",
                table: "tblRulos",
                column: "SentAuthorizerID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRulos_TestResultID",
                table: "tblRulos",
                column: "TestResultID");

            migrationBuilder.CreateIndex(
                name: "IX_tblSamples_CutterID",
                table: "tblSamples",
                column: "CutterID");

            migrationBuilder.CreateIndex(
                name: "IX_tblSamples_RuloProcessID",
                table: "tblSamples",
                column: "RuloProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_tblTestResults_TestCategoryID",
                table: "tblTestResults",
                column: "TestCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserRoles_UserID",
                table: "tblUserRoles",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFloors");

            migrationBuilder.DropTable(
                name: "tblMachines");

            migrationBuilder.DropTable(
                name: "tblPieces");

            migrationBuilder.DropTable(
                name: "tblSamples");

            migrationBuilder.DropTable(
                name: "tblShifts");

            migrationBuilder.DropTable(
                name: "tblUserRoles");

            migrationBuilder.DropTable(
                name: "tblRuloProcesses");

            migrationBuilder.DropTable(
                name: "tblDefinationProcess");

            migrationBuilder.DropTable(
                name: "tblRulos");

            migrationBuilder.DropTable(
                name: "tblPeriods");

            migrationBuilder.DropTable(
                name: "tblTestResults");

            migrationBuilder.DropTable(
                name: "tblUsers");

            migrationBuilder.DropTable(
                name: "tblTestCategories");
        }
    }
}
