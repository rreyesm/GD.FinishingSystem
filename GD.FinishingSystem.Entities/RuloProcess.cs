using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table("tblRuloProcesses")]
    public class RuloProcess : BaseEntity
    {
        public RuloProcess()
        {
            IsFinished = false;
        }
        [Key]
        public int RuloProcessID { get; set; }

        [ForeignKey("RuloID")]
        public Rulo Rulo { get; set; }
        [Display(Name = "Rulo ID")]
        public int RuloID { get; set; }

        [ForeignKey("DefinationProcessID")]
       
        public DefinationProcess DefinationProcess { get; set; }
        [Display(Name = "Definition Process")]
        [Required]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Please select a definition process")]
        public int DefinationProcessID { get; set; }

        [Display(Name = "Beginning Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm}")]
        public DateTime BeginningDate { get; set; }
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm}")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Finish Meter")]
        public decimal? FinishMeter { get; set; }
        [Display(Name = "Is Finished")]
        public bool IsFinished { get; set; }
        public bool IsByMeterCounter { get; set; }

    }
}
