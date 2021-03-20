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
   
        public int RuloProcessID { get; set; }
        [ForeignKey("RuloProcessID")]
        public RuloProcess RuloProcess { get; set; }
        public decimal Meter { get; set; }
        [Display(Name = "Cutter Person")]
        public int CutterID { get; set; }
        [ForeignKey(name:"CutterID")]
        public User CutterUser { get; set; }
        public string Details { get; set; }
    }
}
