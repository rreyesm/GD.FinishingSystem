using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table("tblPeriods")]
    public class Period: BaseEntity
    {
        [Key]
        public int PeriodID { get; set; }
        [Display(Name = "Begin Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Finish Date")]
        public DateTime? FinishDate { get; set; }
        public Int64 LastPeriod { get; set; }
        [NotMapped]
        [Display(Name = "Time Different")]
        public TimeSpan ValidityLastPeriod
        {
            get { return TimeSpan.FromTicks(LastPeriod); }
            set { LastPeriod = value.Ticks; }
        }
        public string Style { get; set; }
        [ForeignKey("SystemPrinterID")]
        public int? SystemPrinterID { get; set; }
        public SystemPrinter SystemPrinter { get; set; }
    }
}
