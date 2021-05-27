using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities.ViewModels
{
    [Table("tblCustomReports")]
    public class TblCustomReport
    {
        [Display(Name="Machine Name")]
        public string Name { get; set; }
        [Display(Name = "Shift")]
        public int Shift { get; set; }
        [Display(Name = "Style")]
        public string Style { get; set; }
        [Display(Name = "Style Name")]
        public string StyleName { get; set; }
        [Display(Name = "Lote")]
        public string Lote { get; set; }
        [Display(Name = "Entrance Length")]
        public decimal? FinishMeterRama { get; set; }
        [Display(Name = "Finish Meter Rulo Process")]
        public decimal? FinishMeterRP { get; set; }
        [Display(Name = "Exit Length")]
        public decimal? ExitLength { get; set; }
    }
}
