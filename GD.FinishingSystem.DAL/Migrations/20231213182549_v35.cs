using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PackingListID",
                table: "tblRuloMigrations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblPackingList",
                columns: table => new
                {
                    PackingListID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_tblPackingList", x => x.PackingListID);
                });

            //migrationBuilder.CreateTable(
            //    name: "VMRuloMigrationReport",
            //    columns: table => new
            //    {
            //        RuloMigrationID = table.Column<int>(type: "int", nullable: false),
            //        Date = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        StyleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NextMachine = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BeamStop = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Beam = table.Column<int>(type: "int", nullable: false),
            //        IsToyotaText = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Loom = table.Column<int>(type: "int", nullable: false),
            //        PieceNo = table.Column<int>(type: "int", nullable: false),
            //        PieceBetilla = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Meters = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        SizingMeters = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        MigrationCategoryID = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        WeavingShift = table.Column<int>(type: "int", nullable: false),
            //        RuloID = table.Column<int>(type: "int", nullable: true),
            //        IsToyota = table.Column<bool>(type: "bit", nullable: false),
            //        Origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        WarehouseCategoryID = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Partiality = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });

            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [dbo].[tblWarehouseCategories] ON 
                GO
                INSERT [dbo].[tblWarehouseCategories] ([WarehouseCategoryID], [WarehouseCode], [Name], [Description], [CreatorID], [CreatedDate], [LastUpdaterID], [LastUpdateDate], [IsDeleted], [DeletedDate], [DeleterID]) VALUES (9, N'W-1', N'W-1', N'Rejected From Finishing To Weaving', 1, CAST(N'2023-12-13T16:50:00.0000000' AS DateTime2), 1, CAST(N'2023-11-13T16:50:00.0000000' AS DateTime2), 0, NULL, NULL)
                INSERT [dbo].[tblWarehouseCategories] ([WarehouseCategoryID], [WarehouseCode], [Name], [Description], [CreatorID], [CreatedDate], [LastUpdaterID], [LastUpdateDate], [IsDeleted], [DeletedDate], [DeleterID]) VALUES (10, N'QC3', N'QC3', N'Cutting In Inspection', 1, CAST(N'2023-12-13T16:50:00.0000000' AS DateTime2), 1, CAST(N'2023-11-13T16:50:00.0000000' AS DateTime2), 0, NULL, NULL)
                GO
                SET IDENTITY_INSERT [dbo].[tblWarehouseCategories] OFF
                GO"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblPackingList");

            //migrationBuilder.DropTable(
            //    name: "VMRuloMigrationReport");

            migrationBuilder.DropColumn(
                name: "PackingListID",
                table: "tblRuloMigrations");
        }
    }
}
