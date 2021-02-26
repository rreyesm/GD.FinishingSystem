using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Entities.ViewModels
{
    public class VMMachine
    {
        public int ID { get; set; }
        public int DefinationProcessID { get; set; }
        [Display(Name = "Process")]
        public string processName { get; set; }
        [Display(Name = "Machine Code")]
        public string MachineCode { get; set; }
        [Display(Name = "Machine Name")]
        public string MachineName { get; set; }
        [Display(Name = "Floor")]
        public string FloorName { get; set; }
    }
}
