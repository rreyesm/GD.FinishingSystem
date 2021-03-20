using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GD.FinishingSystem.Entities
{
    [Table(name:"tblShifts")]
    public class Shift:BaseEntity
    {
        [Key]
        public int ShiftID { get; set; }
        [MaxLength(6), MinLength(1),Required]
        public string ShiftCode { get; set; }
        [Required]
        public TimeSpan StartHour { get; set; }
        [Required]
        public TimeSpan EndHour { get; set; }

    }
}
