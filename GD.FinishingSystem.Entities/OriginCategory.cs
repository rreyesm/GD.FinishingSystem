using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Entities
{
    [Table(name: "tblOriginCategories")]
    public class OriginCategory : BaseEntity
    {
        [Key]
        public int OriginCategoryID { get; set; }
        [MaxLength(5, ErrorMessage = "Origin Code must be a maximum of 5 caracteres")]
        [Required]
        [Display(Name = "Origin Code")]
        public string OriginCode { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
