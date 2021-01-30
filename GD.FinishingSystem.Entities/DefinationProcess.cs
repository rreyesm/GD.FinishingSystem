using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table("tblDefinationProcess")]
    public class DefinationProcess : BaseEntity
    {
        [Key]
        public int DefinationProcessID { get; set; }
        [MaxLength(10)]
        public string ProcessCode { get; set; }
        public string Name { get; set; }

    }
}
