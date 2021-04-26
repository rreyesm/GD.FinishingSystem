using System;
using System.Collections.Generic;

#nullable disable

namespace GD.FinishingSystem.DAL.EFdbPerformanceStandards
{
    public partial class TblParametersMethod
    {
        public TblParametersMethod()
        {
            TblTestDetails = new HashSet<TblTestDetail>();
        }

        public int Id { get; set; }
        public string MethodName { get; set; }
        public int ParameterId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }

        public virtual TblParameter Parameter { get; set; }
        public virtual ICollection<TblTestDetail> TblTestDetails { get; set; }
    }
}
