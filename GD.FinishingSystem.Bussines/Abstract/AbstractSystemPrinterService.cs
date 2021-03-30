using GD.FinishingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Abstract
{
    public abstract class AbstractSystemPrinterService
    {
        public abstract Task<IEnumerable<SystemPrinter>> GetSystemPrinterList();
        public abstract Task<SystemPrinter> GetSystemPrinterFromSystemPrinterID(int systemPrinterID);
        public abstract Task Add(SystemPrinter systemPrinter, int adderRef);
        public abstract Task Update(SystemPrinter systemPrinter, int updaterRef);
        public abstract Task Delete(SystemPrinter systemPrinter, int deleterRef);
        public abstract Task<int> CountByFileName(string fileName);
    }
}
