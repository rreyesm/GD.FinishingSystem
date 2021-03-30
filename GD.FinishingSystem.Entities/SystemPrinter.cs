using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table(name: "tblSystemPrinters")]
    public class SystemPrinter: BaseEntity
    {
        public int SystemPrinterID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Location { get; set; }
        [ForeignKey(name: "FloorID")]
        [Display(Name = "Floor")]
        public int FloorID { get; set; }
        public Floor Floor { get; set; }
    }
}
