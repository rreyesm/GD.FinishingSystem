using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.DAL.EFdbPerformanceStandards
{
    [Keyless]
    [NotMapped]
    public class TblCustomPerformanceForFinishing
    {
        public string ParameterName { get; set; }
        public string MethodName { get; set; }
        public decimal Value { get; set; }
        public bool? Success { get; set; }
        public string Category { get; set; }
        public int Beam { get; set; }

    }
}
