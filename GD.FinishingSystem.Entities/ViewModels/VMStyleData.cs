using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GD.FinishingSystem.Entities.ViewModels
{
    public class VMStyleData
    {
        [Display(Name = "Style")]
        public string Style { get; set; }
        [Display(Name = "Style Name")]
        public string StyleName { get; set; }
        [Display(Name = "Lote")]
        public string Lote { get; set; }
    }
}
