using System;
using System.Collections.Generic;

#nullable disable

namespace GD.FinishingSystem.DAL.EFdbPerformanceStandards
{
    public partial class TblTestDetail
    {
        public int Id { get; set; }
        public int ParameterId { get; set; }
        public int? ParameterMethodId { get; set; }
        public bool Active { get; set; }
        public bool? Success { get; set; }
        public decimal Value { get; set; }
        public decimal ManipulationValue { get; set; }
        public int AdderUserId { get; set; }
        public bool ManipulationChecked { get; set; }
        public int? ManipulationUserId { get; set; }
        public int TestMasterId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }

        public virtual TblParameter Parameter { get; set; }
        public virtual TblParametersMethod ParameterMethod { get; set; }
        public virtual TblTestMaster TestMaster { get; set; }
    }
}
