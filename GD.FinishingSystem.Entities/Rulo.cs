using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

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
        [Display(Name = "Julio")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Julio no válido")]
        public int Beam { get; set; }
        [Display(Name = "Para de Julio")]
        [RegularExpression(@"^(A|B)$", ErrorMessage = "Parada de Julio no valida. Introduzca A o B")]
        public string BeamStop { get; set; }
        //TODO: Salón 2: 101-148, Salón 1: 149-248, Salón 3: 301-324, Salón 4: 401-462
        [RegularExpression(@"^(10[1-9]|1[1-3][0-9]|14[0-8])|(14[9]|1[5-9][0-9]|2[0-3][0-9]|24[0-8])|(30[1-9]|31[0-9]|32[0-4])|(40[1-9]|4[1-5][0-9]|46[0-2])$", ErrorMessage = "El Telar no existe")]
        public int Loom { get; set; }
        [Display(Name = "Es Toyota")]
        public bool IsToyota { get; set; }
        [Display(Name = "Estilo")]
        public string Style { get; set; }
        [Display(Name = "Nombre de Estilo")]
        public string StyleName { get; set; }
        [Display(Name = "Ancho")]
        public decimal Width { get; set; }
        [Display(Name = "Longitud de tejido")]
        public decimal WeavingLength { get; set; }
        [Display(Name = "Longitud de Entrada")]
        [RegularExpression(@"^0*[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$", ErrorMessage = "Metros no válidos")]
        public decimal EntranceLength { get; set; }
        [Display(Name = "Longitud de Salida")]
        public decimal ExitLength { get; set; }
        [Display(Name = "Contracción")]
        public decimal Shrinkage { get; set; }
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Turno no válido")]
        [Display(Name = "Turno")]
        public int Shift { get; set; }
        [Display(Name = "Esperando Respuesta De Pruebas")]
        public bool IsWaitingAnswerFromTest { get; set; }
        [Display(Name = "ID Prueba")]
        public int? TestResultID { get; set; }
        [ForeignKey("TestResultID")]
        public TestResult TestResult { get; set; }
        public int? TestResultAuthorizer { get; set; }
        [Display(Name = "Origen Principal")]
        public int MainOriginID { get; set; }
        [Display(Name = "Origen")]
        public int OriginID { get; set; }
        public string Observations { get; set; }
        [Display(Name = "Número de Folio")]
        public int FolioNumber { get; set; }
        [Display(Name = "Fecha de envío")]
        public DateTime? SentDate { get; set; }
        [Display(Name = "Remitente")]
        public int? SenderID { get; set; }
        [ForeignKey("SenderID")]
        public User Sender { get; set; }
        [Display(Name = "Autorizador de envio")]
        public int? SentAuthorizerID { get; set; }
        [ForeignKey(name: "SentAuthorizerID")]
        public User SentAuthorizer { get; set; }
        [Display(Name = "Contar Pieza")]
        public int PieceCount { get; set; }
        public int PeriodID { get; set; }
        [ForeignKey(name: "PeriodID")]
        public Period Period { get; set; }
        [Display(Name = "Es Prueba")]
        public bool IsTestStyle { get; set; }
        [ForeignKey(name: "WarehouseCategoryID")]
        public WarehouseCategory WarehouseCatgory { get; set; }
        public int? WarehouseCategoryID { get; set; }

    }
}
