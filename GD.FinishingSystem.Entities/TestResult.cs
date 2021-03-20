using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GD.FinishingSystem.Entities
{
    [Table("tblTestResults")]
    public class TestResult : BaseEntity
    {
        [Key]
        public int TestResultID { get; set; }
        [Required, MinLength(length:1)]
        public string Details { get; set; }

        public bool Result { get; set; }
        [Display(Name = "Can Continue")]
        public bool CanContinue { get; set; }
  
        public int TestCategoryID { get; set; }

        [ForeignKey("TestCategoryID")]
        public TestCategory TestCategory { get; set; }

    }
}