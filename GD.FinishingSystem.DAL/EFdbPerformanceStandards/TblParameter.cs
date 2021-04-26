using System;
using System.Collections.Generic;

#nullable disable

namespace GD.FinishingSystem.DAL.EFdbPerformanceStandards
{
    public partial class TblParameter
    {
        public TblParameter()
        {
            TblParametersMethods = new HashSet<TblParametersMethod>();
            TblTestDetails = new HashSet<TblTestDetail>();
        }

        public int Id { get; set; }
        public string ParameterName { get; set; }
        public bool IsMasterParameter { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<TblParametersMethod> TblParametersMethods { get; set; }
        public virtual ICollection<TblTestDetail> TblTestDetails { get; set; }
    }
}
