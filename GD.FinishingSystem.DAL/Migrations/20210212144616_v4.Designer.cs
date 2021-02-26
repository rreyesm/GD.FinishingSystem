﻿// <auto-generated />
using System;
using GD.FinishingSystem.DAL.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GD.FinishingSystem.DAL.Migrations
{
    [DbContext(typeof(FinishingSystemContext))]
    [Migration("20210212144616_v4")]
    partial class v4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("GD.FinishingSystem.Entities.DefinationProcess", b =>
                {
                    b.Property<int>("DefinationProcessID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatorID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProcessCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("DefinationProcessID");

                    b.ToTable("tblDefinationProcess");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.Floor", b =>
                {
                    b.Property<int>("FloorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FloorName")
                        .HasColumnType("TEXT");

                    b.HasKey("FloorID");

                    b.ToTable("tblFloors");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.Machine", b =>
                {
                    b.Property<int>("MachineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatorID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DefinationProcessID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FloorID")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MachineCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("MachineName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MachineID");

                    b.HasIndex("DefinationProcessID");

                    b.ToTable("tblMachines");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.Rulo", b =>
                {
                    b.Property<int>("RuloID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Beam")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatorID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntranceLength")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExitLength")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsWaitingAnswerFromTest")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Loom")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Lote")
                        .HasColumnType("TEXT");

                    b.Property<string>("Observations")
                        .HasColumnType("TEXT");

                    b.Property<int>("Origin")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Piece")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Style")
                        .HasColumnType("TEXT");

                    b.Property<string>("StyleName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TestResultAuthorizer")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TestResultID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Width")
                        .HasColumnType("INTEGER");

                    b.HasKey("RuloID");

                    b.HasIndex("TestResultID");

                    b.ToTable("tblRulos");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.RuloProcess", b =>
                {
                    b.Property<int>("RuloProcessID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BeginningDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatorID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DefinationProcessID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("FinishMeter")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RuloID")
                        .HasColumnType("INTEGER");

                    b.HasKey("RuloProcessID");

                    b.HasIndex("DefinationProcessID");

                    b.HasIndex("RuloID");

                    b.ToTable("tblRuloProcesses");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.TestResult", b =>
                {
                    b.Property<int>("TestResultID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CanContinue")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatorID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Details")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("INTEGER");

                    b.HasKey("TestResultID");

                    b.ToTable("tblTestResults");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatorID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordStored")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("UserID");

                    b.ToTable("tblUsers");
                });

            modelBuilder.Entity("GD.FinishingSystem.Entities.UserRole", b =>
                {
                    b.Property<int>("UserRoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuthorizeType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatorID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeleterID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FormName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("LastUpdaterID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserRoleID");

                    b.HasIndex("UserID");

                    b.ToTable("tblUserRoles");
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

            modelBuilder.Entity("GD.FinishingSystem.Entities.Rulo", b =>
                {
                    b.HasOne("GD.FinishingSystem.Entities.TestResult", "TestResult")
                        .WithMany()
                        .HasForeignKey("TestResultID");

                    b.Navigation("TestResult");
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
