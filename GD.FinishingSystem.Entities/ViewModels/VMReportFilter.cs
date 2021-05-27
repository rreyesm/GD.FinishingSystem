using System;
using System.Collections.Generic;
using System.Text;

namespace GD.FinishingSystem.Entities.ViewModels
{
    public class VMReportFilter
    {
        public DateTime dtBegin { get; set; }
        public DateTime dtEnd { get; set; }
        public int numLote { get; set; }
        public int numBeam { get; set; }
        public string stop { get; set; }
        public int numLoom { get; set; }
        public string txtStyle { get; set; }
        public int shift { get; set; }

        public int typeReport { get; set; }
    }
}
