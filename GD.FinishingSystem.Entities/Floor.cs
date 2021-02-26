using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table(name: "tblFloors")]
    public class Floor : BaseEntity
    {
        [Key]
        public int FloorID { get; set; }
        [Required, Display(Name = "Floor Name")]
        public string FloorName { get; set; }
    }
}
