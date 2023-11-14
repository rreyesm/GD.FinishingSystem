using System;
using System.Collections.Generic;
using System.Text;

namespace GD.FinishingSystem.Entities.ViewModels
{
    public class VMReportFilter: VMRuloFilters
    {
        public int shift { get; set; }
        public int typeReport { get; set; }
    }
}
