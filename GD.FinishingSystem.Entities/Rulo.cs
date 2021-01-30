using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GD.FinishingSystem.Entities
{
    [Table(name: "tblRulos")]
    public class Rulo : BaseEntity
    {
        public Rulo()
        {
            IsWaitingAnswerFromTest = false;

        }
        [Key]
        public int RuloID { get; set; }
        public string Lote { get; set; }
        public int Beam { get; set; }
        public int Loom { get; set; }
        public int Piece { get; set; }
        public string Style { get; set; }
        [Display(Name = "Style Name")]
        public string StyleName { get; set; }
        public int Width { get; set; }
        [Display(Name = "Entrance Length")]
        public int EntranceLength { get; set; }
        [Display(Name = "Exit Length")]
        public int ExitLength { get; set; }
        [Display(Name = "Is Waiting Answer From Test")]
        public bool IsWaitingAnswerFromTest { get; set; }

        [ForeignKey("TestResultID")]
        public TestResult TestResult { get; set; }
        public int? TestResultID { get; set; }
        public int? TestResultAuthorizer { get; set; }



    }
}
