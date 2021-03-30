using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Classes
{
    public class AppSettings
    {
        public bool IsPrinterIP { get; set; }
        public string GD1PrinterName { get; set; }
        public string GD2PrinterName { get; set; }
        public int FileSizeLimit { get; set; }
    }
}
