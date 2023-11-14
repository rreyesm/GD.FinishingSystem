using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Entities
{
    [Table("tblLocations")]
    public class Location : BaseEntity
    {
        [Key]
        public int LocationID { get; set; }
        [ForeignKey(name: "FloorID")]
        public Floor Floor { get; set; }
        public int FloorID { get; set; }
        [MaxLength(10, ErrorMessage = "Máximo 10 carácteres")]
        [MinLength(1, ErrorMessage = "Mínimo 1 carácter")]
        public string Name { get; set; }
    }
}
