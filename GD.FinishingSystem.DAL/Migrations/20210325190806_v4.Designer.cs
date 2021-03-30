﻿// <auto-generated />
using System;
using GD.FinishingSystem.DAL.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GD.FinishingSystem.DAL.Migrations
{
    [DbContext(typeof(FinishingSystemContext))]
    [Migration("20210325190806_v4")]
    partial class v4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GD.FinishingSystem.Entities.DefinationProcess", b =>
                {
                    b.Property<int>("DefinationProcessID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMustSample")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProcessCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("DefinationProcessID");

                    b.ToTable("tblDefinationProcess");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.Floor", b =>
                {
                    b.Property<int>("FloorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("int");

                    b.Property<string>("FloorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("int");

                    b.HasKey("FloorID");

                    b.ToTable("tblFloors");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.Machine", b =>
                {
                    b.Property<int>("MachineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<int>("DefinationProcessID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("int");

                    b.Property<int>("FloorID")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("int");

                    b.Property<string>("MachineCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("MachineName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MachineID");

                    b.HasIndex("DefinationProcessID");

                    b.ToTable("tblMachines");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.MigrationCategory", b =>
                {
                    b.Property<int>("MigrationCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MigrationCategoryID");

                    b.ToTable("tblMigrationCategories");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.MigrationControl", b =>
                {
                    b.Property<int>("MigrationControlId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExcelFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FileRowsTotal")
                        .HasColumnType("int");

                    b.Property<int>("LastMigratedRowOfExcelFile")
                        .HasColumnType("int");

                    b.Property<int>("RowsTotalMigrated")
                        .HasColumnType("int");

                    b.HasKey("MigrationControlId");

                    b.ToTable("tblMigrationControls");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.Period", b =>
                {
                    b.Property<int>("PeriodID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FinishDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long>("LastPeriod")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Style")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PeriodID");

                    b.ToTable("tblPeriods");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.Piece", b =>
                {
                    b.Property<int>("PieceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("int");

                    b.Property<decimal>("Meter")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PieceNo")
                        .HasColumnType("int");

                    b.Property<int>("RuloID")
                        .HasColumnType("int");

                    b.HasKey("PieceID");

                    b.HasIndex("RuloID");

                    b.ToTable("tblPieces");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.Rulo", b =>
                {
                    b.Property<int>("RuloID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Beam")
                        .HasColumnType("int");

                    b.Property<string>("BeamStop")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("int");

                    b.Property<decimal>("EntranceLength")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ExitLength")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("FolioNumber")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsToyota")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWaitingAnswerFromTest")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("int");

                    b.Property<int>("Loom")
                        .HasColumnType("int");

                    b.Property<string>("Lote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observations")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OriginID")
                        .HasColumnType("int");

                    b.Property<int>("PeriodID")
                        .HasColumnType("int");

                    b.Property<int>("PieceCount")
                        .HasColumnType("int");

                    b.Property<int?>("SenderID")
                        .HasColumnType("int");

                    b.Property<int?>("SentAuthorizerID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Shift")
                        .HasColumnType("int");

                    b.Property<string>("Style")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StyleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TestResultAuthorizer")
                        .HasColumnType("int");

                    b.Property<int?>("TestResultID")
                        .HasColumnType("int");

                    b.Property<decimal>("Width")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("RuloID");

                    b.HasIndex("PeriodID");

                    b.HasIndex("SenderID");

                    b.HasIndex("SentAuthorizerID");

                    b.HasIndex("TestResultID");

                    b.ToTable("tblRulos");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.RuloMigration", b =>
                {
                    b.Property<int>("RuloMigrationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Beam")
                        .HasColumnType("int");

                    b.Property<string>("BeamStop")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("int");

                    b.Property<decimal>("GummedMeters")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("int");

                    b.Property<int>("Loom")
                        .HasColumnType("int");

                    b.Property<int>("Lote")
                        .HasColumnType("int");

                    b.Property<decimal>("Meters")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MigrationCategoryID")
                        .HasColumnType("int");

                    b.Property<string>("NextMachine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PieceNo")
                        .HasColumnType("int");

                    b.Property<int?>("RuloID")
                        .HasColumnType("int");

                    b.Property<int>("Shift")
                        .HasColumnType("int");

                    b.Property<string>("Style")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StyleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RuloMigrationID");

                    b.HasIndex("MigrationCategoryID");

                    b.HasIndex("RuloID");

                    b.ToTable("tblRuloMigrations");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.RuloProcess", b =>
                {
                    b.Property<int>("RuloProcessID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BeginningDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<int>("DefinationProcessID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("FinishMeter")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("int");

                    b.Property<int>("RuloID")
                        .HasColumnType("int");

                    b.HasKey("RuloProcessID");

                    b.HasIndex("DefinationProcessID");

                    b.HasIndex("RuloID");

                    b.ToTable("tblRuloProcesses");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.Sample", b =>
                {
                    b.Property<int>("SampleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<int>("CutterID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("int");

                    b.Property<decimal>("Meter")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RuloProcessID")
                        .HasColumnType("int");

                    b.HasKey("SampleID");

                    b.HasIndex("CutterID");

                    b.HasIndex("RuloProcessID");

                    b.ToTable("tblSamples");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.Shift", b =>
                {
                    b.Property<int>("ShiftID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("EndHour")
                        .HasColumnType("time");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("int");

                    b.Property<string>("ShiftCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<TimeSpan>("StartHour")
                        .HasColumnType("time");

                    b.HasKey("ShiftID");

                    b.ToTable("tblShifts");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.TestCategory", b =>
                {
                    b.Property<int>("TestCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TestCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TestCategoryID");

                    b.ToTable("tblTestCategories");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.TestResult", b =>
                {
                    b.Property<int>("TestResultID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CanContinue")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("int");

                    b.Property<bool>("Result")
                        .HasColumnType("bit");

                    b.Property<int>("TestCategoryID")
                        .HasColumnType("int");

                    b.HasKey("TestResultID");

                    b.HasIndex("TestCategoryID");

                    b.ToTable("tblTestResults");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordStored")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("tblUsers");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.UserRole", b =>
                {
                    b.Property<int>("UserRoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorizeType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("int");

                    b.Property<string>("FormName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("UserRoleID");

                    b.HasIndex("UserID");

                    b.ToTable("tblUserRoles");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.ViewModels.VMRuloReport", b =>
                {
                    b.Property<int>("Beam")
                        .HasColumnType("int");

                    b.Property<string>("BeamStop")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CanContinue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("EntranceLength")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ExitLength")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("FolioNumber")
                        .HasColumnType("int");

                    b.Property<string>("IsToyota")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsWaitingAnswerFromTest")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastRuloProcess")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Loom")
                        .HasColumnType("int");

                    b.Property<string>("Lote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PieceCount")
                        .HasColumnType("int");

                    b.Property<int>("RuloID")
                        .HasColumnType("int");

                    b.Property<string>("RuloObservations")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SentAuthorizerID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Shift")
                        .HasColumnType("int");

                    b.Property<string>("Style")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StyleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TestCategoryCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TestResultAuthorizer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TestResultObservations")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Width")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("tblRuloReports");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.Machine", b =>
                {
                    b.HasOne("GD.FinishingSystem.Entities.DefinationProcess", "DefinationProcess")
                        .WithMany()
                        .HasForeignKey("DefinationProcessID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DefinationProcess");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.Piece", b =>
                {
                    b.HasOne("GD.FinishingSystem.Entities.Rulo", "Rulo")
                        .WithMany()
                        .HasForeignKey("RuloID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rulo");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.Rulo", b =>
                {
                    b.HasOne("GD.FinishingSystem.Entities.Period", "Period")
                        .WithMany()
                        .HasForeignKey("PeriodID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GD.FinishingSystem.Entities.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderID");

                    b.HasOne("GD.FinishingSystem.Entities.User", "SentAuthorizer")
                        .WithMany()
                        .HasForeignKey("SentAuthorizerID");

                    b.HasOne("GD.FinishingSystem.Entities.TestResult", "TestResult")
                        .WithMany()
                        .HasForeignKey("TestResultID");

                    b.Navigation("Period");

                    b.Navigation("Sender");

                    b.Navigation("SentAuthorizer");

                    b.Navigation("TestResult");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.RuloMigration", b =>
                {
                    b.HasOne("GD.FinishingSystem.Entities.MigrationCategory", "MigrationCategory")
                        .WithMany()
                        .HasForeignKey("MigrationCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GD.FinishingSystem.Entities.Rulo", "Rulo")
                        .WithMany()
                        .HasForeignKey("RuloID");

                    b.Navigation("MigrationCategory");

                    b.Navigation("Rulo");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.RuloProcess", b =>
                {
                    b.HasOne("GD.FinishingSystem.Entities.DefinationProcess", "DefinationProcess")
                        .WithMany()
                        .HasForeignKey("DefinationProcessID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GD.FinishingSystem.Entities.Rulo", "Rulo")
                        .WithMany()
                        .HasForeignKey("RuloID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DefinationProcess");

                    b.Navigation("Rulo");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.Sample", b =>
                {
                    b.HasOne("GD.FinishingSystem.Entities.User", "CutterUser")
                        .WithMany()
                        .HasForeignKey("CutterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GD.FinishingSystem.Entities.RuloProcess", "RuloProcess")
                        .WithMany()
                        .HasForeignKey("RuloProcessID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CutterUser");

                    b.Navigation("RuloProcess");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.TestResult", b =>
                {
                    b.HasOne("GD.FinishingSystem.Entities.TestCategory", "TestCategory")
                        .WithMany()
                        .HasForeignKey("TestCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestCategory");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.UserRole", b =>
                {
                    b.HasOne("GD.FinishingSystem.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
