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
    public class VMRulo
    {
        public int RuloID { get; set; }
        public string Lote { get; set; }
        public int Beam { get; set; }
        [Display(Name = "Beam Stop")]
        public string BeamStop { get; set; }
        public int Loom { get; set; }
        public bool IsToyotaValue { get; set; }
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
        [Display(Name = "Exit Minus Samples")]
        public decimal ExitLengthMinusSamples { get; set; }
        [Display(Name = "Shrinkage %")]
        public decimal Shrinkage { get; set; }
        [Display(Name = "Meter Contraction %")]
        public decimal MeterContraction { get; set; }
        [Display(Name = "Contraction Weaving-Exit Finish")]
        public decimal ContractionWeavingExitFinish { get; set; }
        [Display(Name = "Inspection Length")]
        public decimal InspectionLength { get; set; }
        [Display(Name = "Insp. Cutting Length")]
        public decimal InspectionCuttingLength { get; set; }
        public int Shift { get; set; }
        public bool IsWaitingAnswerFromTestValue { get; set; } //Agregado de VMRulo
        [Display(Name = "Is Waiting Answer From Test")]
        public string IsWaitingAnswerFromTest { get; set; }
        public int? TestResultID { get; set; } //Agregado de VMRulo
        public bool CanContinueValue { get; set; }//Agregado de VMRulo
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
        [Display(Name = "Main Origin")]
        public string MainOriginCode { get; set; }
        [Display(Name = "OriginID")]
        public int OriginID { get; set; } //Agregado de VMRulo
        [Display(Name = "Origin")]
        public string OriginCode { get; set; }
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
        public int? SentAuthorizerID { get; set; }  //Agregado de VMRulo
        [Display(Name = "Creator")]
        public string Creator { get; set; }
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
        public DateTime? FinishingDate { get; set; }
        [Display(Name = "Machine")]
        public string Machine { get; set; }
        [Display(Name = "Warehouse")]
        public string WarehouseCode { get; set; }
        [Display(Name = "Inspection Start Date")]
        public DateTime? InspectionStartDate { get; set; }
        [Display(Name = "Inspection Finish Date")]
        public DateTime? InspectionFinishDate { get; set; }
        [Display(Name = "Cutting Start Date")]
        public DateTime? CuttingStartDate { get; set; }
        [Display(Name = "Cutting Finish Date")]
        public DateTime? CuttingFinishDate { get; set; }
    }
}
