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
            Samples = new SampleManager(context);
            TestCategories = new TestCategoryManager(context);
            TestResults = new TestResultManager(context);
            Users = new UserManager(context);

        }

        public AbstractDefinationProcessService DefinationProcesses { get; set; }
        public AbstractFloorService Floors { get; set; }
        public AbstractMachineService Machines { get; set; }
        public AbstractPeriodService Periods { get; set; }
        public AbstractPieceService Pieces { get; set; }
        public AbstractRuloService Rulos { get; set; }
        public AbstractSampleService Samples { get; set; }
        public AbstractTestCategoryService TestCategories { get; set; }
        public AbstractTestResultService TestResults { get; set; }
        public AbstractUserService Users { get; set; }
    }
}
