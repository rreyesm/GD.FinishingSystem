using System.ComponentModel.DataAnnotations;

namespace GD.FinishingSystem.Entities
{
    public class TestResult:BaseEntity
    {
        [Key]
        public int TestResultID { get; set; }
        public string Details { get; set; }
        public bool CanContinue { get; set; }
    }
}