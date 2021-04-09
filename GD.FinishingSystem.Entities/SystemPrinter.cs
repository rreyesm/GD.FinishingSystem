using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table(name: "tblSystemPrinters")]
    public class SystemPrinter : BaseEntity
    {
        [Key]
        public int SystemPrinterID { get; set; }
        [Display(Name = "Printer Name")]
        public string Name { get; set; }
        [Display(Name = "Is Printer IP")]
        public bool IsPrinterIP { get; set; }
        public string Location { get; set; }
        [ForeignKey(name: "FloorID")]
        [Display(Name = "Floor")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Please select a floor")]
        public int FloorID { get; set; }
        public Floor Floor { get; set; }
        [RegularExpression(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$", ErrorMessage = "IP no valid!")]
        [Required]
        [Display(Name ="PC IP")]
        public string PCIP { get; set; }
        [ForeignKey(name: "MachineID")]
        [Display(Name = "Machine")]
        public int? MachineID { get; set; }
        public Machine Machine { get; set; }

    }
}
