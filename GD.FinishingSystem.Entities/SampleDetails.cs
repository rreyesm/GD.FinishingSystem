using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table("tblSampleDetails")]
    public class SampleDetail: BaseEntity
    {
        [Key]
        public int SampleDetailID { get; set; }
        [ForeignKey("SampleID")]
        public int SampleID { get; set; }
        [ForeignKey("RuloID")]
        public int RuloID { get; set; }
        [ForeignKey("RuloProcessID")]
        public int RuloProcessID { get; set; }
        public decimal Meter { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm}")]
        public DateTime DateTime { get; set; }
        public string Details { get; set; }
    }
}
