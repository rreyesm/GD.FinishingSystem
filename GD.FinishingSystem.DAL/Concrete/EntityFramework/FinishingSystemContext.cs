using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GD.FinishingSystem.DAL.Concrete.EntityFramework
{
    public class FinishingSystemContext : DbContext
    {
        #region Definations
        static string DatabaseName = "dbFinishingSystem";
        public string ConnectionStringProp { private get; set; }

//#if DEBUG
//        string LocalSqlServer = $"Server=.;Database={DatabaseName};User Id=SA;Password=0545696sS*;Connection Timeout=0";
//#else
    string LocalSqlServer = $"Server=192.168.7.242;Database={DatabaseName};User Id=EMY;Password=0545696s;Connection Timeout=0";
//#endif

        string LocalSqlite = $"Data Source=C://Users//Sistemas//{DatabaseName}.db";
        int TimeOutAuthorizeSec = 600;
        DatabaseSystem dbSysem;
        #endregion


        #region DebugModeSettings
        /// <summary>
        /// Sqlite local database ( FOR TESTS )
        /// </summary>
        public FinishingSystemContext()
        {
            //ConnectionStringProp = LocalSqlite;
            //dbSysem = DatabaseSystem.Sqlite;
            ConnectionStringProp = LocalSqlServer;
            dbSysem = DatabaseSystem.SqlServer;
        }

        /// <summary>
        /// Sqlserver or Sqlite local connections ( FOR TESTS )
        /// </summary>
        /// <param name="system"></param>
        public FinishingSystemContext(DatabaseSystem system)
        {
            switch (system)
            {
                case DatabaseSystem.SqlServer:
                    ConnectionStringProp = LocalSqlServer;
                    break;
                case DatabaseSystem.Sqlite:
                    ConnectionStringProp = LocalSqlite;

                    break;
                default:
                    break;
            }
            dbSysem = system;
        }

        #endregion


        #region ReleaseModeSettings
        /// <summary>
        /// Release connection
        /// </summary>
        /// <param name="system">Database system</param>
        /// <param name="ConnectionString">Connection string</param>
        public FinishingSystemContext(DatabaseSystem system, string ConnectionString)
        {
            dbSysem = system;
            ConnectionStringProp = ConnectionString;
        }
        #endregion



        #region dbSets
        public DbSet<DefinationProcess> DefinationProcesses { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<MigrationCategory> MigrationCategories { get; set; }
        public DbSet<MigrationControl> MigrationControls { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Piece> Pieces { get; set; }
        public DbSet<Rulo> Rulos { get; set; }
        public DbSet<RuloProcess> RuloProcesses { get; set; }
        public DbSet<RuloMigration> RuloMigrations { get; set; }
        public DbSet<VMRulo> RuloReports { get; set; }
        public DbSet<Sample> Samples { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<TestCategory> TestCategories { get; set; }
        public DbSet<SystemPrinter> SystemPrinters { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<VMRuloBatch> RuloBatches { get; set; }
        #endregion

        #region Settings
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (dbSysem)
            {
                case DatabaseSystem.SqlServer:
                    optionsBuilder.UseSqlServer(ConnectionStringProp, builder =>
                    {
                        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(TimeOutAuthorizeSec), null);

                    });
                    base.OnConfiguring(optionsBuilder);
                    break;
                case DatabaseSystem.Sqlite:
                    optionsBuilder.UseSqlite(ConnectionStringProp); //, builder => builder.CommandTimeout((int?)TimeOutAuthorizeSec));
                    break;
                default:
                    break;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VMRulo>().HasNoKey();

            //Custom table
            modelBuilder.Entity<TblCustomReport>().HasNoKey();

            ////modelBuilder.Entity<TblCustomReport>(entity =>
            ////{
            ////    entity.HasNoKey();
            ////    entity.ToTable("TblCustomReport");
            ////    entity.Property(e => e.Name).HasMaxLength(150);
            ////    entity.Property(e => e.Shift);
            ////    entity.Property(e => e.Style).HasMaxLength(20);
            ////    entity.Property(e => e.StyleName).HasMaxLength(150);
            ////    entity.Property(e => e.Lote).HasMaxLength(10);
            ////    entity.Property(e => e.FinishMeterRama);
            ////    entity.Property(e => e.FinishMeterRP);
            ////    entity.Property(e => e.ExitLength);
            ////});

            modelBuilder.Entity<VMRuloBatch>().HasNoKey();

            modelBuilder.Entity<TotalResult>().HasNoKey();
        }


        #endregion

        #region SaveChangesOverrides
        public override int SaveChanges()
        {
            var entities = (from entry in ChangeTracker.Entries()
                            where entry.State == EntityState.Modified || entry.State == EntityState.Added || EntityState.Deleted == entry.State
                            select entry.Entity);

            var validationResults = new List<ValidationResult>();
            foreach (var entity in entities)
            {
                if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
                {
                    if (Debugger.IsAttached)
                    {
                        Debug.Indent();

                        foreach (var valid in validationResults)
                        {
                            Debug.WriteLine(string.Join(",", valid.MemberNames) + " is not validated! Error:" + valid.ErrorMessage);
                        }

                        Debug.Unindent();
                    }
                }
            }
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = (from entry in ChangeTracker.Entries()
                            where entry.State == EntityState.Modified || entry.State == EntityState.Added || EntityState.Deleted == entry.State
                            select entry.Entity);

            var validationResults = new List<ValidationResult>();
            foreach (var entity in entities)
            {
                if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
                {
                    if (Debugger.IsAttached)
                    {
                        Debug.Indent();
                        foreach (var valid in validationResults)
                        {
                            Debug.WriteLine(string.Join(",", valid.MemberNames) + " is not validated! Error:" + valid.ErrorMessage);
                        }

                        Debug.Unindent();
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        #endregion

        public enum DatabaseSystem
        {
            SqlServer,
            Sqlite
        }
    }
}
