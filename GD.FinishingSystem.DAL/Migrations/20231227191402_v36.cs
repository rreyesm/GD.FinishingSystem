using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v36 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblWarehouseKNCategories",
                columns: table => new
                {
                    WarehouseKNCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_tblWarehouseKNCategories", x => x.WarehouseKNCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "tblReprocesses",
                columns: table => new
                {
                    ReprocessID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseKNCategoryID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PieceID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WithoutPzaID = table.Column<bool>(type: "bit", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StyleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Splice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PpHsy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OnzYd2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Beam = table.Column<int>(type: "int", nullable: false),
                    Loom = table.Column<int>(type: "int", nullable: false),
                    Pallet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Meters = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MainOriginID = table.Column<int>(type: "int", nullable: false),
                    OriginID = table.Column<int>(type: "int", nullable: false),
                    RollObs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PalletObs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FloorID = table.Column<int>(type: "int", nullable: false),
                    FinishingMachine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginFinishingNumber = table.Column<int>(type: "int", nullable: false),
                    OriginPartiRef = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_tblReprocesses", x => x.ReprocessID);
                    table.ForeignKey(
                        name: "FK_tblReprocesses_tblFloors_FloorID",
                        column: x => x.FloorID,
                        principalTable: "tblFloors",
                        principalColumn: "FloorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblReprocesses_tblOriginCategories_MainOriginID",
                        column: x => x.MainOriginID,
                        principalTable: "tblOriginCategories",
                        principalColumn: "OriginCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblReprocesses_tblOriginCategories_OriginID",
                        column: x => x.OriginID,
                        principalTable: "tblOriginCategories",
                        principalColumn: "OriginCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tblReprocesses_tblWarehouseKNCategories_WarehouseKNCategoryID",
                        column: x => x.WarehouseKNCategoryID,
                        principalTable: "tblWarehouseKNCategories",
                        principalColumn: "WarehouseKNCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblReprocesses_FloorID",
                table: "tblReprocesses",
                column: "FloorID");

            migrationBuilder.CreateIndex(
                name: "IX_tblReprocesses_MainOriginID",
                table: "tblReprocesses",
                column: "MainOriginID");

            migrationBuilder.CreateIndex(
                name: "IX_tblReprocesses_OriginID",
                table: "tblReprocesses",
                column: "OriginID");

            migrationBuilder.CreateIndex(
                name: "IX_tblReprocesses_WarehouseKNCategoryID",
                table: "tblReprocesses",
                column: "WarehouseKNCategoryID");

            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [dbo].[tblWarehouseKNCategories] ON
                GO
                INSERT [dbo].[tblWarehouseKNCategories] ([WarehouseKNCategoryID], [WarehouseCode], [Name], [Description], [CreatorID], [CreatedDate], [LastUpdaterID], [LastUpdateDate], [IsDeleted], [DeletedDate], [DeleterID]) VALUES (1, N'RT', N'RT', N'Return From Inspection To Finishing', 1, CAST(N'2022-12-27T13:30:00.0000000' AS DateTime2), 1, CAST(N'2023-12-27T13:30:00.0000000' AS DateTime2), 0, NULL, NULL)
                GO
                SET IDENTITY_INSERT [dbo].[tblWarehouseKNCategories] OFF
                GO
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblReprocesses");

            migrationBuilder.DropTable(
                name: "tblWarehouseKNCategories");
        }
    }
}
