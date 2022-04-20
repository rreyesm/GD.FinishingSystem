using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GD.FinishingSystem.DAL.EFdbPerformanceStandards
{
    public partial class dbPerformanceStandardsContext : DbContext
    {
        public dbPerformanceStandardsContext()
        {
        }

        public dbPerformanceStandardsContext(DbContextOptions<dbPerformanceStandardsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblParameter> TblParameters { get; set; }
        public virtual DbSet<TblParametersMethod> TblParametersMethods { get; set; }
        public virtual DbSet<TblTestDetail> TblTestDetails { get; set; }
        public virtual DbSet<TblTestMaster> TblTestMasters { get; set; }

        public virtual DbSet<TblCustomPerformanceForFinishing> TblCustomPerformanceForFinishings { get; set; }
        public virtual DbSet<TblCustomPerformanceMasiveForFinishing> TblCustomPerformanceMasiveForFinishing { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=192.168.7.242;Initial Catalog=dbPerformanceStandards;User Id=emy;password=0545696s");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblParameter>(entity =>
            {
                entity.ToTable("tblParameters");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ParameterName).HasMaxLength(100);
            });

            modelBuilder.Entity<TblParametersMethod>(entity =>
            {
                entity.ToTable("tblParametersMethods");

                entity.HasIndex(e => e.ParameterId, "IX_ParameterId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.MethodName).HasMaxLength(100);

                entity.HasOne(d => d.Parameter)
                    .WithMany(p => p.TblParametersMethods)
                    .HasForeignKey(d => d.ParameterId)
                    .HasConstraintName("FK_dbo.tblParametersMethods_dbo.tblParameters_ParameterId");
            });

            modelBuilder.Entity<TblTestDetail>(entity =>
            {
                entity.ToTable("tblTestDetails");

                entity.HasIndex(e => e.AdderUserId, "IX_AdderUserId");

                entity.HasIndex(e => e.ManipulationUserId, "IX_ManipulationUserId");

                entity.HasIndex(e => e.ParameterId, "IX_ParameterId");

                entity.HasIndex(e => e.ParameterMethodId, "IX_ParameterMethodId");

                entity.HasIndex(e => e.TestMasterId, "IX_TestMasterId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ManipulationValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Parameter)
                    .WithMany(p => p.TblTestDetails)
                    .HasForeignKey(d => d.ParameterId)
                    .HasConstraintName("FK_dbo.tblTestDetails_dbo.tblParameters_ParameterId");

                entity.HasOne(d => d.ParameterMethod)
                    .WithMany(p => p.TblTestDetails)
                    .HasForeignKey(d => d.ParameterMethodId)
                    .HasConstraintName("FK_dbo.tblTestDetails_dbo.tblParametersMethods_ParameterMethodId");

                entity.HasOne(d => d.TestMaster)
                    .WithMany(p => p.TblTestDetails)
                    .HasForeignKey(d => d.TestMasterId)
                    .HasConstraintName("FK_dbo.tblTestDetails_dbo.tblTestMasters_TestMasterId");
            });

            modelBuilder.Entity<TblTestMaster>(entity =>
            {
                entity.ToTable("tblTestMasters");

                entity.HasIndex(e => e.CustomerId, "IX_CustomerId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.NewStyle).HasColumnName("New_Style");

                entity.Property(e => e.ResultMessage).HasColumnName("Result_Message");
            });

            //Custom table
            modelBuilder.Entity<TblCustomPerformanceForFinishing>();
            modelBuilder.Entity<TblCustomPerformanceMasiveForFinishing>();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
