using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Entities.ViewModels
{
    public class WarehouseStock
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Stock { get; set; }
    }
}
