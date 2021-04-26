using System;
using System.Collections.Generic;

#nullable disable

namespace GD.FinishingSystem.DAL.EFdbPerformanceStandards
{
    public partial class TblTestMaster
    {
        public TblTestMaster()
        {
            TblTestDetails = new HashSet<TblTestDetail>();
        }

        public int Id { get; set; }
        public int Lote { get; set; }
        public int Loom { get; set; }
        public int Beam { get; set; }
        public string Style { get; set; }
        public string NewStyle { get; set; }
        public bool Success { get; set; }
        public bool SuccessIsMain { get; set; }
        public bool SuccessIsDynamic { get; set; }
        public string ResultMessage { get; set; }
        public string Category { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<TblTestDetail> TblTestDetails { get; set; }
    }
}
