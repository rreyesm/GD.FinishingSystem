using GD.FinishingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
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
        [Display(Name = "Julio")]
        public int Beam { get; set; }
        [Display(Name = "Parada de Julio")]
        public string BeamStop { get; set; }
        [Display(Name = "Telar")]
        public int Loom { get; set; }
        public bool IsToyotaValue { get; set; }
        [Display(Name = "Es Toyota")]
        public string IsToyota { get; set; }
        [Display(Name = "Piezas")]
        public int PieceCount { get; set; }
        [Display(Name = "Estilo")]
        public string Style { get; set; }
        [Display(Name = "Nombre de Estilo")]
        public string StyleName { get; set; }
        [Display(Name = "Ancho")]
        public decimal Width { get; set; }
        [Display(Name = "Longitud de Tejido")]
        public decimal WeavingLength { get; set; }
        [Display(Name = "Longitud de Entrada")]
        public decimal EntranceLength { get; set; }
        [Display(Name = "Longitud de Salida Rama")]
        public decimal ExitLengthRama { get; set; }
        [Display(Name = "Longitud de Salida")]
        public decimal ExitLength { get; set; }
        [Display(Name = "Salida Menos Muestras")]
        public decimal ExitLengthMinusSamples { get; set; }
        [Display(Name = "Contracción %")]
        public decimal Shrinkage { get; set; }
        [Display(Name = "Contracción de Metros %")]
        public decimal MeterContraction { get; set; }
        [Display(Name = "Contracción Tejido-Salida Acabado")]
        public decimal ContractionWeavingExitFinish { get; set; }
        [Display(Name = "Longitud de Inspección")]
        public decimal InspectionLength { get; set; }
        [Display(Name = "Longitud de Corte de Insp.")]
        public decimal InspectionCuttingLength { get; set; }
        [Display(Name = "Turno")]
        public int Shift { get; set; }
        public bool IsWaitingAnswerFromTestValue { get; set; } //Agregado de VMRulo
        [Display(Name = "Esperando Respuesta de Pruebas")]
        public string IsWaitingAnswerFromTest { get; set; }
        public int? TestResultID { get; set; } //Agregado de VMRulo
        public bool CanContinueValue { get; set; }//Agregado de VMRulo
        [Display(Name = "Puede Continuar")]
        public string CanContinue { get; set; }
        [NotMapped]
        public int TestCategoryID { get; set; }
        [Display(Name = "Resiltado de Pruebas")]
        public string TestCategoryCode { get; set; }
        [Display(Name = "Fecha de Pruebas")]
        public DateTime? DateTestResult { get; set; }
        [Display(Name = "Numeros de Batch")]
        public string BatchNumbers { get; set; }
        [Display(Name = "Origen Principal")]
        public string MainOriginCode { get; set; }
        [Display(Name = "OrigenID")]
        public int OriginID { get; set; } //Agregado de VMRulo
        [Display(Name = "Origen")]
        public string OriginCode { get; set; }
        [Display(Name = "Observaciones de Rulo")]
        public string RuloObservations { get; set; }
        [Display(Name = "Autorizador de Resultados de Pruebas")]
        public string TestResultAuthorizer { get; set; }
        [Display(Name = "Observaciones de Resultados de Pruebas")]
        public string TestResultObservations { get; set; }
        [Display(Name = "Numero de Folio")]
        public int FolioNumber { get; set; }
        [Display(Name = "Fecha de Envio")]
        public DateTime? SentDate { get; set; }
        [Display(Name = "Remitente")]
        public string Sender { get; set; }
        [Display(Name = "Autorizador de Envío")]
        public string SentAuthorizer { get; set; }
        public int? SentAuthorizerID { get; set; }  //Agregado de VMRulo
        [Display(Name = "Creador")]
        public string Creator { get; set; }
        [Display(Name = "Fecha de Creación")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Fecha de Finalización")]
        public DateTime? FinishingDate { get; set; }
        [Display(Name = "Máquina")]
        public string Machine { get; set; }
        [Display(Name = "Bodega")]
        public string WarehouseCode { get; set; }
        [Display(Name = "Fecha de Inicio de Insp.")]
        public DateTime? InspectionStartDate { get; set; }
        [Display(Name = "Fecha de Finalización Insp.")]
        public DateTime? InspectionFinishDate { get; set; }
        [Display(Name = "Fecha de Inicio de Corte")]
        public DateTime? CuttingStartDate { get; set; }
        [Display(Name = "Fecha de Finalización de Corte")]
        public DateTime? CuttingFinishDate { get; set; }
    }
}
