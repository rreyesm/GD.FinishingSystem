using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GD.FinishingSystem.Entities
{
    [Table("tblTestResults")]
    public class TestResult:BaseEntity
    {
        [Key]
        public int TestResultID { get; set; }
        public string Details { get; set; }
        [Display(Name = "Can Continue")]
        public bool CanContinue { get; set; }

        [NotMapped]
        public int RelRuloId { get; set; }
    }
}