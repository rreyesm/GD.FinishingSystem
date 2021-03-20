using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GD.FinishingSystem.Entities.ViewModels
{
    public class VMRuloProcess
    {
        public int RuloProcessID { get; set; }
        public int RuloID { get; set; }
        public DefinationProcess DefinationProcess { get; set; }
        [Display(Name = "Definition Process")]
        [Required]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Please select a definition process")]
        public int DefinationProcessID { get; set; }
        [Display(Name = "Beginning Date")]
        public DateTime BeginningDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Finish Meter")]
        public decimal? FinishMeter { get; set; }
        [Display(Name = "Is Finished")]
        public bool IsFinished { get; set; }
        public bool IsMustSample { get; set; }
        public bool ExistSample { get; set; }
    }
}
