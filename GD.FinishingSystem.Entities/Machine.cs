using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table("tblMachines")]
   public class Machine: BaseEntity
    {
        [Key]
        public int MachineID { get; set; }
        public int DefinationProcessID { get; set; }
        public DefinationProcess DefinationProcess { get; set; }
        [MaxLength(10)]
        [Display(Name = "Machine Code")]
        public string MachineCode { get; set; }
        [Display(Name = "Machine Name")]
        public string MachineName { get; set; }

    }
}
