using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table("tblMachines")]
    public class Machine : BaseEntity
    {
        [Key]
        public int MachineID { get; set; }
        public int DefinationProcessID { get; set; }
        [ForeignKey(name: "DefinationProcessID")]
        public DefinationProcess DefinationProcess { get; set; }
        [MaxLength(10)]
        [Display(Name = "Machine Code")]
        [Required]
        public string MachineCode { get; set; }
        [Display(Name = "Machine Name")]
        [Required]
        public string MachineName { get; set; }
        [ForeignKey("FloorID")]
        [Display(Name = "Floor")]
        [Required]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Please select a floor")]
        public int FloorID { get; set; }
    }
}
