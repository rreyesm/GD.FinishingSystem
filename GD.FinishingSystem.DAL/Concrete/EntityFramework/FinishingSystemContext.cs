using GD.FinishingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        string LocalSqlServer = $"Server=.;Database={DatabaseName};User Id=SA;Password=0545696s;";
        string LocalSqlite = $"Data Source=C://{DatabaseName}.db";
        DatabaseSystem dbSysem;
        #endregion


        #region DebugModeSettings
        /// <summary>
        /// Sqlite local database ( FOR TESTS )
        /// </summary>
        public FinishingSystemContext()
        {
            ConnectionStringProp = LocalSqlite;
            dbSysem = DatabaseSystem.Sqlite;
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
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<Rulo> Rulos { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<DefinationProcess> DefinationProcesses { get; set; }
        public DbSet<RuloProcess> RuloProcesses { get; set; }

        #endregion

        #region Settings
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (dbSysem)
            {
                case DatabaseSystem.SqlServer:
                    optionsBuilder.UseSqlServer(ConnectionStringProp, builder =>
                    {
                        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);

                    });
                    base.OnConfiguring(optionsBuilder);
                    break;
                case DatabaseSystem.Sqlite:
                    optionsBuilder.UseSqlite(ConnectionStringProp);
                    break;
                default:
                    break;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


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
