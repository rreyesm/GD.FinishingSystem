using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace GD.FinishingSystem.Entities.ViewModels
{
    [NotMapped]
    [Keyless]
    [Table("tblRuloBatches")]
    public class VMRuloBatch
    {
        public int RuloID { get; set; }
        public string BatchNumbers { get; set; }
        public decimal Inspectionlength { get; set; }
        public decimal CuttingLenght { get; set; }
    }
}
