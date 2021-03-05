using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table("tblSamples")]
    public class Sample: BaseEntity
    {
        [Key]
        public int SampleID { get; set; }
        [ForeignKey("RuloProcessID")]
        public int RuloProcessID { get; set; }
        public decimal Meter { get; set; }
        [Display(Name = "Cutter Person")]
        public string CutterPerson { get; set; }
        public string Details { get; set; }
    }
}
