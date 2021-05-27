using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities.ViewModels
{
    public class VMPeriodReport
    {
        public int ID { get; set; }
        public string Style { get; set; }
        [Display(Name = "Begin Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Finish Date")]
        public DateTime? FinishDate { get; set; }
        public Int64 LastPeriod { get; set; }
        [Display(Name = "Time Different")]
        public TimeSpan ValidityLastPeriod
        {
            get { return TimeSpan.FromTicks(LastPeriod); }
            set { LastPeriod = value.Ticks; }
        }
    }
}
