using System;
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
        [Display(Name = "Beam Stop")]
        public string BeamStop { get; set; }
        //TODO: Salón 2: 101-148, Salón 1: 149-248, Salón 3: 301-324, Salón 4: 401-462
        [RegularExpression(@"^(10[1-9]|1[1-3][0-9]|14[0-8])|(14[9]|1[5-9][0-9]|2[0-3][0-9]|24[0-8])|(30[1-9]|31[0-9]|32[0-4])|(40[1-9]|4[1-5][0-9]|46[0-2])$", ErrorMessage = "Loom does not exist")]
        public int Loom { get; set; }
        [Display(Name = "Is Toyota")]
        public bool IsToyota { get; set; }
        public string Style { get; set; }
        [Display(Name = "Style Name")]
        public string StyleName { get; set; }
        public decimal Width { get; set; }
        [Display(Name = "Entrance Length")]
        public decimal EntranceLength { get; set; }
        [Display(Name = "Exit Length")]
        public decimal ExitLength { get; set; }
        public int Shift { get; set; }
        [Display(Name = "Is Waiting Answer From Test")]

        public bool IsWaitingAnswerFromTest { get; set; }
        public int? TestResultID { get; set; }
        [ForeignKey("TestResultID")]
        public TestResult TestResult { get; set; }
        public int? TestResultAuthorizer { get; set; }
        [Display(Name = "Origin")]
        public int OriginID { get; set; }
        public string Observations { get; set; }
        [Display(Name = "Folio Number")]
        public int FolioNumber { get; set; }
        [Display(Name = "Sent Date")]
        public DateTime? SentDate { get; set; }
        public int? SenderID { get; set; }
        [ForeignKey("SenderID")]
        public User Sender { get; set; }
        [Display(Name = "Sent Authorizer")]
        public int? SentAuthorizerID { get; set; }
        [ForeignKey(name: "SentAuthorizerID")]
        public User SentAuthorizer { get; set; }
        public int PieceCount { get; set; }
        public int PeriodID { get; set; }
        [ForeignKey(name: "PeriodID")]
        public Period Period { get; set; }
        [Display(Name = "Is Test")]
        public bool IsTestStyle { get; set; }
    }
}
