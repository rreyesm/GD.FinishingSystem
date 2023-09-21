using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Entities
{
    [Table(name: "tblWarehouseCategories")]
    public class WarehouseCategory : BaseEntity
    {
        [Key]
        public int WarehouseCategoryID { get; set; }
        [Display(Name = "Warehouse Code")]
        public string WarehouseCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
