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
#if RELEASE
            throw new Exception("You can not use this constructor in Release Mode");
#endif
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
            Users = new UserManager(context);
            Rulos = new RuloManager(context);
            DefinationProcesses = new DefinationProcessManager(context);
            Machines = new MachineManager(context);
            TestResults = new TestResultManager(context);
        }

        public AbstractUserService Users { get; set; }
        public AbstractRuloService Rulos { get; set; }

        public AbstractDefinationProcessService DefinationProcesses { get; set; }
        public AbstractMachineService Machines { get; set; }
        public AbstractTestResultService TestResults { get; set; }

    }
}
