using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Entities.ViewModels
{
    public class VMPerformanceTestReport
    {
        public int RuloID { get; set; }
        public string Lote { get; set; }
        public int Beam { get; set; }
        public int Loom { get; set; }
        public string ParameterName { get; set; }
        public string MethodName { get; set; }
        public decimal Value { get; set; }
        public bool? Success { get; set; }
        public string Category { get; set; }
        public int TestBeam { get; set; }
    }
}
