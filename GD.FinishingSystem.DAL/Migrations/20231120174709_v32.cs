using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblLocations_tblFloors_FloorID",
                table: "tblLocations");

            //migrationBuilder.RenameColumn(
            //    name: "FinishMeterRama",
            //    table: "tblCustomReports",
            //    newName: "ExitRamaChamuscado");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tblLocations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FloorID",
                table: "tblLocations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            //migrationBuilder.AddColumn<decimal>(
            //    name: "EntranceLength",
            //    table: "tblCustomReports",
            //    type: "decimal(18,2)",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "MainOrigin",
            //    table: "tblCustomReports",
            //    type: "nvarchar(max)",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Origin",
            //    table: "tblCustomReports",
            //    type: "nvarchar(max)",
            //    nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "tblFinishRawFabricEntrances",
            //    columns: table => new
            //    {
            //        Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        StyleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Salon1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        Salon2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        Salon3 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        Salon4 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        TotalGeneral = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });

            migrationBuilder.AddForeignKey(
                name: "FK_tblLocations_tblFloors_FloorID",
                table: "tblLocations",
                column: "FloorID",
                principalTable: "tblFloors",
                principalColumn: "FloorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [dbo].[tblWarehouseCategories] ON 
                GO
                INSERT [dbo].[tblWarehouseCategories] ([WarehouseCategoryID], [WarehouseCode], [Name], [Description], [CreatorID], [CreatedDate], [LastUpdaterID], [LastUpdateDate], [IsDeleted], [DeletedDate], [DeleterID]) VALUES (8, N'W1', N'W1', N'In Stock Weaving', 1, CAST(N'2023-11-15T12:10:00.0000000' AS DateTime2), 1, CAST(N'2023-11-15T12:10:00.0000000' AS DateTime2), 0, NULL, NULL)
                GO
                SET IDENTITY_INSERT [dbo].[tblWarehouseCategories] OFF
                GO"
                );

            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [tblLocations] ON
                GO
                INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (297,'Pasillo A-B ',1,'2023-11-20 15:00',1,'2023-11-20 15:00',0,NULL,NULL,1)
                INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (298,'Pasillo C-D ',1,'2023-11-20 15:00',1,'2023-11-20 15:00',0,NULL,NULL,1)
                INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (299,'Area de Carga ',1,'2023-11-20 15:00',1,'2023-11-20 15:00',0,NULL,NULL,1)
                INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (300,'Pasillo 1 (escritorio)',1,'2023-11-20 15:00',1,'2023-11-20 15:00',0,NULL,NULL,1)
                INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (301,'Pasillo 2 (lavanderia)',1,'2023-11-20 15:00',1,'2023-11-20 15:00',0,NULL,NULL,1)
                INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (302,'Pasillo A-B ',1,'2023-11-20 15:00',1,'2023-11-20 15:00',0,NULL,NULL,2)
                INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (303,'Pasillo C-D ',1,'2023-11-20 15:00',1,'2023-11-20 15:00',0,NULL,NULL,2)
                INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (304,'Tepango ',1,'2023-11-20 15:00',1,'2023-11-20 15:00',0,NULL,NULL,2)
                INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (305,'Mario crosta ',1,'2023-11-20 15:00',1,'2023-11-20 15:00',0,NULL,NULL,2)
                INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (306,'Penteck ',1,'2023-11-20 15:00',1,'2023-11-20 15:00',0,NULL,NULL,2)
                INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (307,'Puerta de Tejido 1',1,'2023-11-20 15:00',1,'2023-11-20 15:00',0,NULL,NULL,2)
                INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (308,'Pizo A1',1,'2023-11-20 15:00',1,'2023-11-20 15:00',0,NULL,NULL,2)
                GO
                SET IDENTITY_INSERT [tblLocations] OFF
                GO"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblLocations_tblFloors_FloorID",
                table: "tblLocations");

            //migrationBuilder.DropTable(
            //    name: "tblFinishRawFabricEntrances");

            //migrationBuilder.DropColumn(
            //    name: "EntranceLength",
            //    table: "tblCustomReports");

            //migrationBuilder.DropColumn(
            //    name: "MainOrigin",
            //    table: "tblCustomReports");

            //migrationBuilder.DropColumn(
            //    name: "Origin",
            //    table: "tblCustomReports");

            //migrationBuilder.RenameColumn(
            //    name: "ExitRamaChamuscado",
            //    table: "tblCustomReports",
            //    newName: "FinishMeterRama");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tblLocations",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FloorID",
                table: "tblLocations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tblLocations_tblFloors_FloorID",
                table: "tblLocations",
                column: "FloorID",
                principalTable: "tblFloors",
                principalColumn: "FloorID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
