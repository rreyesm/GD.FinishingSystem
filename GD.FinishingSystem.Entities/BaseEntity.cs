using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    public class BaseEntity
    {
        [Required]
        public int CreatorID { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public int LastUpdaterID { get; set; }
        [Required]
        public DateTime LastUpdateDate { get; set; }
        [Required]
        public bool IsDeleted { get; set; } 
        public DateTime? DeletedDate { get; set; }
        public int? DeleterID { get; set; }

    }
}
