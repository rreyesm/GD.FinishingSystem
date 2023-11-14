using Microsoft.EntityFrameworkCore.Migrations;

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblLocation",
                table: "tblLocation");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "tblRuloMigrations");

            migrationBuilder.RenameTable(
                name: "tblLocation",
                newName: "tblLocations");

            migrationBuilder.AddColumn<int>(
                name: "LocationID",
                table: "tblRuloMigrations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FloorID",
                table: "tblLocations",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblLocations",
                table: "tblLocations",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRuloMigrations_LocationID",
                table: "tblRuloMigrations",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_tblLocations_FloorID",
                table: "tblLocations",
                column: "FloorID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblLocations_tblFloors_FloorID",
                table: "tblLocations",
                column: "FloorID",
                principalTable: "tblFloors",
                principalColumn: "FloorID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblRuloMigrations_tblLocations_LocationID",
                table: "tblRuloMigrations",
                column: "LocationID",
                principalTable: "tblLocations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql(
                @"
                    SET IDENTITY_INSERT dbo.tblLocations ON
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (1,'A-1',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (2,'A-2',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (3,'A-3',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (4,'A-4',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (5,'A-5',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (6,'A-6',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (7,'A-7',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (8,'A-8',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (9,'A-9',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (10,'A-10',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (11,'A-11',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (12,'A-12',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (13,'A-13',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (14,'A-14',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (15,'A-15',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (16,'A-16',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (17,'A-17',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (18,'A-18',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (19,'A-19',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (20,'A-20',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (21,'A-21',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (22,'A-22',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (23,'A-23',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (24,'A-24',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (25,'A-25',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (26,'A-26',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (27,'A-27',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (28,'A-28',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (29,'A-29',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (30,'A-30',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (31,'A-31',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (32,'A-32',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (33,'A-33',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (34,'A-34',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (35,'A-35',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (36,'A-36',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (37,'B-1',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (38,'B-2',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (39,'B-3',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (40,'B-4',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (41,'B-5',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (42,'B-6',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (43,'B-7',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (44,'B-8',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (45,'B-9',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (46,'B-10',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (47,'B-11',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (48,'B-12',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (49,'B-13',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (50,'B-14',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (51,'B-15',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (52,'B-16',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (53,'B-17',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (54,'B-18',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (55,'C-1',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (56,'C-2',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (57,'C-3',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (58,'C-4',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (59,'C-5',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (60,'C-6',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (61,'C-7',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (62,'C-8',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (63,'C-9',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (64,'C-10',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (65,'C-11',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (66,'C-12',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (67,'C-13',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (68,'C-14',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (69,'C-15',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (70,'D-1',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (71,'D-2',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (72,'D-3',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (73,'D-4',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (74,'D-5',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (75,'D-6',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (76,'D-7',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (77,'D-8',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (78,'D-9',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (79,'D-10',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (80,'D-11',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (81,'D-12',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (82,'D-13',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (83,'D-14',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (84,'E-1',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (85,'E-2',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (86,'E-3',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (87,'E-4',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (88,'E-5',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (89,'E-6',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,1)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (90,'A-1',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (91,'A-2',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (92,'A-3',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (93,'A-4',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (94,'A-5',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (95,'A-6',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (96,'A-7',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (97,'A-8',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (98,'A-9',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (99,'A-10',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (100,'A-11',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (101,'A-12',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (102,'A-13',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (103,'A-14',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (104,'A-15',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (105,'A-16',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (106,'A-17',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (107,'A-18',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (108,'A-19',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (109,'A-20',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (110,'A-21',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (111,'A-22',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (112,'A-23',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (113,'A-24',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (114,'A-25',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (115,'A-26',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (116,'A-27',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (117,'A-28',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (118,'A-29',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (119,'A-30',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (120,'A-31',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (121,'A-32',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (122,'A-33',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (123,'A-34',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (124,'A-35',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (125,'A-36',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (126,'A-37',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (127,'A-38',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (128,'A-39',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (129,'A-40',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (130,'A-41',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (131,'A-42',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (132,'A-43',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (133,'A-44',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (134,'A-45',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (135,'A-46',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (136,'A-47',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (137,'A-48',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (138,'B-49',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (139,'B-50',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (140,'B-51',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (141,'B-52',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (142,'B-53',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (143,'B-54',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (144,'B-55',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (145,'B-56',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (146,'B-57',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (147,'B-58',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (148,'B-59',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (149,'B-60',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (150,'B-61',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (151,'B-62',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (152,'B-63',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (153,'B-64',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (154,'B-65',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (155,'B-66',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (156,'B-67',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (157,'B-68',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (158,'B-69',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (159,'B-70',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (160,'B-71',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (161,'B-72',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (162,'B-73',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (163,'B-74',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (164,'B-75',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (165,'B-76',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (166,'B-77',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (167,'B-78',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (168,'B-79',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (169,'B-80',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (170,'B-81',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (171,'B-82',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (172,'B-83',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (173,'B-84',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (174,'B-85',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (175,'B-86',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (176,'B-87',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (177,'B-88',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (178,'B-89',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (179,'B-90',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (180,'B-91',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (181,'B-92',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (182,'B-93',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (183,'B-94',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (184,'B-95',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (185,'B-96',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (186,'B-97',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (187,'B-98',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (188,'B-99',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (189,'C-100',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (190,'C-101',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (191,'C-102',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (192,'C-103',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (193,'C-104',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (194,'C-105',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (195,'C-106',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (196,'C-107',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (197,'C-108',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (198,'C-109',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (199,'C-110',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (200,'C-111',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (201,'C-112',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (202,'C-113',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (203,'C-114',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (204,'C-115',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (205,'C-116',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (206,'C-117',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (207,'C-118',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (208,'C-119',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (209,'C-120',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (210,'C-121',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (211,'C-122',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (212,'C-123',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (213,'C-124',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (214,'C-125',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (215,'C-126',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (216,'C-127',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (217,'C-128',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (218,'C-129',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (219,'C-130',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (220,'C-131',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (221,'C-132',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (222,'C-133',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (223,'C-134',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (224,'C-135',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (225,'C-136',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (226,'C-137',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (227,'C-138',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (228,'C-139',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (229,'C-140',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (230,'C-141',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (231,'C-142',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (232,'C-143',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (233,'C-144',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (234,'C-145',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (235,'C-146',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (236,'C-147',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (237,'C-148',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (238,'C-149',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (239,'C-150',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (240,'C-151',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (241,'C-152',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (242,'C-153',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (243,'D-154',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (244,'D-155',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (245,'D-156',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (246,'D-157',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (247,'D-158',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (248,'D-159',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (249,'D-160',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (250,'D-161',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (251,'D-162',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (252,'D-163',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (253,'D-164',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (254,'D-165',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (255,'D-166',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (256,'D-167',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (257,'D-168',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (258,'D-169',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (259,'D-170',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (260,'D-171',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (261,'D-172',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (262,'D-173',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (263,'D-174',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (264,'D-175',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (265,'D-176',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (266,'D-177',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (267,'D-178',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (268,'D-179',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (269,'D-180',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (270,'D-181',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (271,'D-182',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (272,'D-183',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (273,'D-184',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (274,'D-185',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (275,'D-186',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (276,'D-187',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (277,'D-188',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (278,'D-189',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (279,'D-190',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (280,'D-191',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (281,'D-192',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (282,'D-193',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (283,'D-194',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (284,'D-195',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (285,'D-196',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (286,'D-197',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (287,'D-198',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (288,'D-199',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (289,'D-200',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (290,'D-201',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (291,'D-202',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (292,'D-203',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (293,'D-204',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (294,'D-205',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (295,'D-206',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    INSERT INTO [dbo].[tblLocations] ([LocationID],[Name],[CreatorID],[CreatedDate],[LastUpdaterID],[LastUpdateDate],[IsDeleted],[DeletedDate],[DeleterID],[FloorID]) VALUES (296,'D-207',1,'2023-11-09 15:00',1,'2023-11-09 15:00',0,NULL,NULL,2)
                    SET IDENTITY_INSERT dbo.tblLocations OFF
                    GO
                "
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblLocations_tblFloors_FloorID",
                table: "tblLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_tblRuloMigrations_tblLocations_LocationID",
                table: "tblRuloMigrations");

            migrationBuilder.DropIndex(
                name: "IX_tblRuloMigrations_LocationID",
                table: "tblRuloMigrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblLocations",
                table: "tblLocations");

            migrationBuilder.DropIndex(
                name: "IX_tblLocations_FloorID",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "LocationID",
                table: "tblRuloMigrations");

            migrationBuilder.DropColumn(
                name: "FloorID",
                table: "tblLocations");

            migrationBuilder.RenameTable(
                name: "tblLocations",
                newName: "tblLocation");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "tblRuloMigrations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblLocation",
                table: "tblLocation",
                column: "LocationID");
        }
    }
}
