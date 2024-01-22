using GD.FinishingSystem.Bussines.Abstract;
using GD.FinishingSystem.Bussines.Concrete;
using GD.FinishingSystem.DAL.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GD.FinishingSystem.Bussines
{
    public class FinishingSystemFactory
    {
        DbContext context;

        /// <summary>
        /// Default constructor using Entity Framework
        /// </summary>
        public FinishingSystemFactory()
        {
            //#if RELEASE
            //            throw new Exception("You can not use this constructor in Release Mode");
            //#endif
            context = new FinishingSystemContext();
            InitObjects();
        }

        public FinishingSystemFactory(string connectionString)
        {
            context = new FinishingSystemContext(FinishingSystemContext.DatabaseSystem.SqlServer, connectionString);
            InitObjects();
        }


        /// <summary>
        /// For release mode
        /// </summary>
        /// <param name="prmContext">Write your selected context</param>
        public FinishingSystemFactory(DbContext prmContext)
        {
            this.context = prmContext;
            InitObjects();
        }
        private void InitObjects()
        {
            DefinationProcesses = new DefinationProcessManager(context);
            Floors = new FloorManager(context);
            Machines = new MachineManager(context);
            Periods = new PeriodManager(context);
            Pieces = new PieceManager(context);
            Rulos = new RuloManager(context);
            RuloMigrations = new RuloMigrationsManager(context);
            Samples = new SampleManager(context);
            SystemPrinters = new SystemPrinterManager(context);
            TestCategories = new TestCategoryManager(context);
            TestResults = new TestResultManager(context);
            Users = new UserManager(context);
            OriginCategories = new OriginCategoryManager(context);
            WarehouseCategories = new WarehouseCategoryManager(context);
            PackingList = new PackingListManager(context);
            Reprocesses = new ReprocessManager(context);
        }

        public AbstractDefinationProcessService DefinationProcesses { get; set; }
        public AbstractFloorService Floors { get; set; }
        public AbstractMachineService Machines { get; set; }
        public AbstractPeriodService Periods { get; set; }
        public AbstractPieceService Pieces { get; set; }
        public AbstractRuloService Rulos { get; set; }
        public AbstractRuloMigrationService RuloMigrations { get; set; }
        public AbstractSampleService Samples { get; set; }
        public AbstractSystemPrinterService SystemPrinters { get; set; }
        public AbstractTestCategoryService TestCategories { get; set; }
        public AbstractTestResultService TestResults { get; set; }
        public AbstractUserService Users { get; set; }
        public AbstractOriginCategoryService OriginCategories { get; set; }
        public AbstractWarehouseCategoryService WarehouseCategories { get; set; }
        public AbstractPackingListService PackingList { get; set; }
        public AbstractReprocessService Reprocesses { get; set; }
    }
}
