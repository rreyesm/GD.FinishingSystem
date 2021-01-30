using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table("tblMachines")]
   public class Machine
    {
        [Key]
        public int MachineID { get; set; }
        public int DefinationProcessID { get; set; }
        public DefinationProcess DefinationProcess { get; set; }
        [MaxLength(10)]
        public string MachineCode { get; set; }
        public string MachineName { get; set; }

    }
}
