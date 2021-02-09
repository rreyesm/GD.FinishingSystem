using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table(name: "tblFloors")]
    public class Floor
    {
        [Key]
        public int FloorID { get; set; }
        public string FloorName { get; set; }
    }
}
