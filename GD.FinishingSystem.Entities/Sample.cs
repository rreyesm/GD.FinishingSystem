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
        [ForeignKey("RuloID")]
        public int RuloID { get; set; }
        [ForeignKey("RuloProcessID")]
        public int RuloProcessID { get; set; }
        public decimal Meter { get; set; }
        public DateTime DateTime { get; set; }
    }
}
