using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Models
{
    public class PerformanceTestReport
    {
        public int TestMasterId { get; set; }
        public int MyProperty { get; set; }
        public string ParameterName { get; set; }
        public string MethodName { get; set; }
        public decimal Value { get; set; }
        public bool? Success { get; set; }
        public string Category { get; set; }
    }
}
