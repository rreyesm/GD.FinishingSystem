using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Entities
{
    [Table("tblWarehouseKNCategories")]
    public class WarehouseKNCategory : BaseEntity
    {
        [Key]
        public int WarehouseKNCategoryID { get; set; }
        [Display(Name = "WarehouseKN Code")]
        [Required]
        public string WarehouseCode { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
