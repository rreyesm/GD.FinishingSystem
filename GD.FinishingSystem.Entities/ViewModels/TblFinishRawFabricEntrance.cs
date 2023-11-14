using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Entities.ViewModels
{
    [NotMapped]
    [Keyless]
    [Table("tblFinishRawFabricEntrances")]
    public class TblFinishRawFabricEntrance
    {
        public string Style { get; set; }
        public string StyleName { get; set; }
        public string Lote { get; set; }
        [Display(Name = "Salon 1 (149-248)")]
        public decimal? Salon1 { get; set; }
        [Display(Name = "Salon 2 (101-148)")]
        public decimal? Salon2 { get; set; }
        [Display(Name = "Salon 3 (301-324)")]
        public decimal? Salon3 { get; set; }
        [Display(Name = "Salon 4 (401-462)")]
        public decimal? Salon4 { get; set; }
        public decimal? TotalGeneral { get; set; }
    }
}
