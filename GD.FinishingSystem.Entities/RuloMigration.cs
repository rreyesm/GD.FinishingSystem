using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table("tblRuloMigrations")]
    public class RuloMigration : BaseEntity
    {
        [Key]
        [Display(Name = "Rulo Migration ID")]
        public int RuloMigrationID { get; set; }
        [Display(Name = "Excel File Row")]
        public int ExcelFileRow { get; set; }
        [Display(Name = "ExcelFileName")]
        public string ExcelFileName { get; set; }
        [Required]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm}")]
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }
        [Display(Name = "Acounting Date")]
        public DateTime? AccountingDate { get; set; } //Agregado para no mover las horas en los reportes
        [Display(Name = "Maquina")]
        public string NextMachine { get; set; }
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Lote no válido")]
        public string Lote { get; set; }
        [Display(Name = "Parada de Julio")]
        [RegularExpression(@"^(A|B)$", ErrorMessage = "Parada de Julio no válida. Introduce A o B")]
        public string BeamStop { get; set; }
        [Display(Name = "Julio")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Julio no válido")]
        public int Beam { get; set; }
        [Display(Name = "Telar")]
        //TODO: Salón 2: 101-148, Salón 1: 149-248, Salón 3: 301-324, Salón 4: 401-462
        [RegularExpression(@"^(10[1-9]|1[1-3][0-9]|14[0-8])|(14[9]|1[5-9][0-9]|2[0-3][0-9]|24[0-8])|(30[1-9]|31[0-9]|32[0-4])|(40[1-9]|4[1-5][0-9]|46[0-2])$", ErrorMessage = "El Telar no existe")]
        public int Loom { get; set; }
        [Display(Name = "Es Toyota")]
        public string IsToyotaText { get; set; }
        [Display(Name = "Estilo")]
        public string Style { get; set; }
        [Display(Name = "Nombre del estilo")]
        public string StyleName { get; set; }
        [Display(Name = "Número de Pieza")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Pieza no válida")]
        public int PieceNo { get; set; }
        [RegularExpression(@"^(B)$", ErrorMessage = "Betilla no válida. Debes introducir el valor B")]
        [Display(Name = "Betilla")]
        public string PieceBetilla { get; set; }
        [Display(Name = "Metros")]
        [RegularExpression(@"^0*[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$", ErrorMessage = "Metros no válidos")]
        public decimal Meters { get; set; }
        [Display(Name = "Metros de Engomado")]
        [RegularExpression(@"^0*[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$+", ErrorMessage = "Metros de engomado no válidos")]
        public decimal SizingMeters { get; set; } //Before Gummed Meters
        [Display(Name = "Estatus")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Por favor selecciona un estatus")]
        public int MigrationCategoryID { get; set; }
        [ForeignKey(name: "MigrationCategoryID")]
        public MigrationCategory MigrationCategory { get; set; }
        [Display(Name = "Observaciones")]
        public string Observations { get; set; }
        [Display(Name = "Turno de Tejido")]
        //[RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Turno no válido")]
        public int WeavingShift { get; set; }
        public int? RuloID { get; set; }
        [ForeignKey(name: "RuloID")]
        public Rulo Rulo { get; set; }
        [Display(Name = "Es Prueba")]
        public bool IsTestStyle { get; set; }
        [Display(Name = "Máquina")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Por favor selecciona una máquina")]
        public int? DefinitionProcessID { get; set; }
        [ForeignKey(name: "DefinitionProcessID")]
        public DefinationProcess DefinitionProcess { get; set; }
        [Display(Name = "Es Toyota")]
        public bool IsToyotaMigration { get; set; }
        [ForeignKey(name: "OriginID")]
        public OriginCategory OriginCategory { get; set; }
        [Display(Name = "Origen")]
        public int? OriginID { get; set; }
        [Display(Name = "Almacén")]
        [ForeignKey(name: "WarehouseCategoryID")]
        public WarehouseCategory WarehouseCatgory { get; set; }
        [Display(Name = "Almacén")]
        public int? WarehouseCategoryID { get; set; }
        [Display(Name = "Parcialidad")]
        public int? Partiality { get; set; }
        [Display(Name = "Avance de Tela")]
        public bool FabricAdvance { get; set; }
        [Display(Name = "Ubicación")]
        [ForeignKey("LocationID")]
        public Location Location { get; set; }
        [Display(Name = "Ubicación")]
        public int? LocationID { get; set; }
<<<<<<< Updated upstream
        [Display(Name = "Packing List No")]
=======
>>>>>>> Stashed changes
        public int? PackingListID { get; set; }

    }
}
