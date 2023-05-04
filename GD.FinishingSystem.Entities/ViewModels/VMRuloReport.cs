using GD.FinishingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Entities.ViewModels
{
    [NotMapped]
    [Keyless]
    [Table("tblRuloReports")]
    public class VMRuloReport
    {
        public int RuloID { get; set; }
        public string Lote { get; set; }
        public int Beam { get; set; }
        [Display(Name = "Beam Stop")]
        public string BeamStop { get; set; }
        public int Loom { get; set; }
        [Display(Name = "Is Toyota")]
        public string IsToyota { get; set; }
        public int PieceCount { get; set; }
        public string Style { get; set; }
        [Display(Name = "Style Name")]
        public string StyleName { get; set; }
        public decimal Width { get; set; }
        [Display(Name = "Weaving Length")]
        public decimal WeavingLength { get; set; }
        [Display(Name = "Entrance Length")]
        public decimal EntranceLength { get; set; }
        [Display(Name = "Exit Length Rama")]
        public decimal ExitLengthRama { get; set; }
        [Display(Name = "Exit Length")]
        public decimal ExitLength { get; set; }
        [Display(Name = "Inspection Length")]
        public decimal InspectionLength { get; set; }
        public int Shift { get; set; }
        [Display(Name = "Is Waiting Answer From Test")]
        public string IsWaitingAnswerFromTest { get; set; }
        [Display(Name = "Can Continue")]
        public string CanContinue { get; set; }
        [NotMapped]
        public int TestCategoryID { get; set; }
        [Display(Name = "Test Result")]
        public string TestCategoryCode { get; set; }
        [Display(Name = "Date Test")]
        public DateTime? DateTestResult { get; set; }
        [Display(Name = "Batch Numbers")]
        public string BatchNumbers { get; set; }
        [Display(Name = "Origin")]
        public string Origin { get; set; }
        [Display(Name = "Rulo Observations")]
        public string RuloObservations { get; set; }
        [Display(Name = "Test Result Authorizer")]
        public string TestResultAuthorizer { get; set; }
        [Display(Name = "Test Result Observations")]
        public string TestResultObservations { get; set; }
        [Display(Name = "Folio Number")]
        public int FolioNumber { get; set; }
        [Display(Name = "Sent Date")]
        public DateTime? SentDate { get; set; }
        [Display(Name = "Sender")]
        public string Sender { get; set; }
        [Display(Name = "Sent Authorizer")]
        public string SentAuthorizer { get; set; }
        [Display(Name = "Creator")]
        public string Creator { get; set; }
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Machine")]
        public string Machine { get; set; }
    }
}
