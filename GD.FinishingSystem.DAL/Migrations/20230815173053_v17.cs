using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tblWarehouseCategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "tblOriginCategories",
                columns: table => new
                {
                    OriginCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdaterID = table.Column<int>(type: "int", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterID = table.Column<int>(type: "int", nullable: true)
                });

            migrationBuilder.Sql(
                @"
                    SET IDENTITY_INSERT dbo.tblWarehouseCategories ON
                    INSERT INTO dbo.tblWarehouseCategories (WarehouseCategoryID,WarehouseCode,[Name],[Description],CreatorID,CreatedDate,LastUpdaterID,LastUpdateDate,IsDeleted,DeletedDate,DeleterID) VALUES (1,'RAW1','RAW','From Warehouse Weaving',1,'20230815 12:10:00',1,'20230815 12:10:00',0,null,null)
                    INSERT INTO dbo.tblWarehouseCategories (WarehouseCategoryID,WarehouseCode,[Name],[Description],CreatorID,CreatedDate,LastUpdaterID,LastUpdateDate,IsDeleted,DeletedDate,DeleterID) VALUES (2,'RAW2','RAW','Pre-Processing',1,'20230815 12:10:00',1,'20230815 12:10:00',0,null,null)
                    INSERT INTO dbo.tblWarehouseCategories (WarehouseCategoryID,WarehouseCode,[Name],[Description],CreatorID,CreatedDate,LastUpdaterID,LastUpdateDate,IsDeleted,DeletedDate,DeleterID) VALUES (3,'A1','A1','Floor Finishing',1,'20230815 12:10:00',1,'20230815 12:10:00',0,null,null)
                    INSERT INTO dbo.tblWarehouseCategories (WarehouseCategoryID,WarehouseCode,[Name],[Description],CreatorID,CreatedDate,LastUpdaterID,LastUpdateDate,IsDeleted,DeletedDate,DeleterID) VALUES (4,'A2','A2','Fabric Waiting',1,'20230815 12:10:00',1,'20230815 12:10:00',0,null,null)
                    INSERT INTO dbo.tblWarehouseCategories (WarehouseCategoryID,WarehouseCode,[Name],[Description],CreatorID,CreatedDate,LastUpdaterID,LastUpdateDate,IsDeleted,DeletedDate,DeleterID) VALUES (5,'A3','A3','Can Continue',1,'20230815 12:10:00',1,'20230815 12:10:00',0,null,null)
                    INSERT INTO dbo.tblWarehouseCategories (WarehouseCategoryID,WarehouseCode,[Name],[Description],CreatorID,CreatedDate,LastUpdaterID,LastUpdateDate,IsDeleted,DeletedDate,DeleterID) VALUES (6,'QC1','QC1','Stock For Inspection',1,'20230815 12:10:00',1,'20230815 12:10:00',0,null,null)
                    INSERT INTO dbo.tblWarehouseCategories (WarehouseCategoryID,WarehouseCode,[Name],[Description],CreatorID,CreatedDate,LastUpdaterID,LastUpdateDate,IsDeleted,DeletedDate,DeleterID) VALUES (7,'QC2','QC2','Fabric Cutting',1,'20230815 12:10:00',1,'20230815 12:10:00',0,null,null)
                    SET IDENTITY_INSERT dbo.tblWarehouseCategories OFF
                    GO

                    SET IDENTITY_INSERT dbo.tblOriginCategories ON
                    INSERT INTO dbo.tblOriginCategories (OriginCategoryID,OriginCode,[Name],[Description],CreatorID,CreatedDate,LastUpdaterID,LastUpdateDate,IsDeleted,DeletedDate,DeleterID) VALUES (1,'PP00','PP00','Process',1,'20230815 12:10:00',1,'20230815 12:10:00',0,null,null)
                    INSERT INTO dbo.tblOriginCategories (OriginCategoryID,OriginCode,[Name],[Description],CreatorID,CreatedDate,LastUpdaterID,LastUpdateDate,IsDeleted,DeletedDate,DeleterID) VALUES (2,'PF00','PF00','Inspection Process and Internal Process',1,'20230815 12:10:00',1,'20230815 12:10:00',0,null,null)
                    INSERT INTO dbo.tblOriginCategories (OriginCategoryID,OriginCode,[Name],[Description],CreatorID,CreatedDate,LastUpdaterID,LastUpdateDate,IsDeleted,DeletedDate,DeleterID) VALUES (3,'TF02','TF02','Quality Recovery',1,'20230815 12:10:00',1,'20230815 12:10:00',0,null,null)
                    INSERT INTO dbo.tblOriginCategories (OriginCategoryID,OriginCode,[Name],[Description],CreatorID,CreatedDate,LastUpdaterID,LastUpdateDate,IsDeleted,DeletedDate,DeleterID) VALUES (4,'DVF2','DVF2','Quality Recovery',1,'20230815 12:10:00',1,'20230815 12:10:00',0,null,null)
                    INSERT INTO dbo.tblOriginCategories (OriginCategoryID,OriginCode,[Name],[Description],CreatorID,CreatedDate,LastUpdaterID,LastUpdateDate,IsDeleted,DeletedDate,DeleterID) VALUES (5,'PF01','PF01','From inspection stock or integration stock',1,'20230815 12:10:00',1,'20230815 12:10:00',0,null,null)
                    INSERT INTO dbo.tblOriginCategories (OriginCategoryID,OriginCode,[Name],[Description],CreatorID,CreatedDate,LastUpdaterID,LastUpdateDate,IsDeleted,DeletedDate,DeleterID) VALUES (6,'PF02','PF02','From inspection stock or integration stock',1,'20230815 12:10:00',1,'20230815 12:10:00',0,null,null)
                    INSERT INTO dbo.tblOriginCategories (OriginCategoryID,OriginCode,[Name],[Description],CreatorID,CreatedDate,LastUpdaterID,LastUpdateDate,IsDeleted,DeletedDate,DeleterID) VALUES (7,'DES0','DES0','For Factory Tests',1,'20230815 12:10:00',1,'20230815 12:10:00',0,null,null)
                    INSERT INTO dbo.tblOriginCategories (OriginCategoryID,OriginCode,[Name],[Description],CreatorID,CreatedDate,LastUpdaterID,LastUpdateDate,IsDeleted,DeletedDate,DeleterID) VALUES (8,'DES1','DES1','Intern Test Reprocess',1,'20230820 12:10:00',1,'20230815 12:10:00',0,null,null)
                    INSERT INTO dbo.tblOriginCategories (OriginCategoryID,OriginCode,[Name],[Description],CreatorID,CreatedDate,LastUpdaterID,LastUpdateDate,IsDeleted,DeletedDate,DeleterID) VALUES (9,'DES2','DES2','Inspection Test Reprocess',1,'20230821 12:10:00',1,'20230815 12:10:00',0,null,null)
                    SET IDENTITY_INSERT dbo.tblOriginCategories OFF
                    GO
                "
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tblWarehouseCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: false);

            migrationBuilder.DropTable(
                name: "tblOriginCategories");
        }
    }
}
