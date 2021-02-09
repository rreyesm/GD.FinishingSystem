using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GD.FinishingSystem.Entities
{
    [Table("tblDefinationProcess")]
    public class DefinationProcess : BaseEntity
    {
        [Key]
        [Display(Name = "Defination Process ID")]
        public int DefinationProcessID { get; set; }
        [MaxLength(10)]
        [Display(Name = "Process Code")]
        public string ProcessCode { get; set; }
        public string Name { get; set; }

    }
}
