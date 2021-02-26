using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table(name: "tblTestCategories")]
    public class TestCategory: BaseEntity
    {
        [Key]
        public int TestCategoryID { get; set; }
        [Required]
        [Display(Name="Test Code")]
        public string TestCode { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
