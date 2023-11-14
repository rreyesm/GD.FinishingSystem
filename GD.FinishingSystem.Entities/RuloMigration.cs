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
        public DateTime Date { get; set; }
        [Display(Name = "Machine")]
        public string NextMachine { get; set; }
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Lote no valid!")]
        public string Lote { get; set; }
        [Display(Name = "Beam Stop")]
        [RegularExpression(@"^(A|B)$", ErrorMessage = "Beam Stop not valid. Enter A or B")]
        public string BeamStop { get; set; }
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Beam no valid!")]
        public int Beam { get; set; }
        //TODO: Salón 2: 101-148, Salón 1: 149-248, Salón 3: 301-324, Salón 4: 401-462
        [RegularExpression(@"^(10[1-9]|1[1-3][0-9]|14[0-8])|(14[9]|1[5-9][0-9]|2[0-3][0-9]|24[0-8])|(30[1-9]|31[0-9]|32[0-4])|(40[1-9]|4[1-5][0-9]|46[0-2])$", ErrorMessage = "Loom does not exist")]
        public int Loom { get; set; }
        [Display(Name = "Is Toyota")]
        public string IsToyotaText { get; set; }
        public string Style { get; set; }
        [Display(Name = "Style Name")]
        public string StyleName { get; set; }
        [Display(Name = "Piece No")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Piece no valid!")]
        public int PieceNo { get; set; }
        [RegularExpression(@"^(B)$", ErrorMessage = "Betilla is not valid. You must enter value B")]
        [Display(Name = "Betilla")]
        public string PieceBetilla { get; set; }
        [RegularExpression(@"^[0-9]*\.?[0-9]+$", ErrorMessage = "Meters no valid!")]
        public decimal Meters { get; set; }
        [Display(Name = "Gummed Meters")]
        public decimal GummedMeters { get; set; }
        [Display(Name = "Status")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Please select an Status")]
        public int MigrationCategoryID { get; set; }
        [ForeignKey(name: "MigrationCategoryID")]
        public MigrationCategory MigrationCategory { get; set; }
        public string Observations { get; set; }
        [Display(Name = "Weaving Shift")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Shift no valid!")]
        public int WeavingShift { get; set; }
        public int? RuloID { get; set; }
        [ForeignKey(name: "RuloID")]
        public Rulo Rulo { get; set; }
        [Display(Name = "Is Test")]
        public bool IsTestStyle { get; set; }
        [Display(Name = "Next Machine")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Please select an Machine")]
        public int? DefinitionProcessID { get; set; }
        [ForeignKey(name: "DefinitionProcessID")]
        public DefinationProcess DefinitionProcess { get; set; }
        [Display(Name = "Is Toyota")]
        public bool IsToyotaMigration { get; set; }
        [ForeignKey(name: "OriginID")]
        public OriginCategory OriginCategory { get; set; }
        [Display(Name = "Origin")]
        public int? OriginID { get; set; }
        [ForeignKey(name: "WarehouseCategoryID")]
        public WarehouseCategory WarehouseCatgory { get; set; }
        public int? WarehouseCategoryID { get; set; }
        public int? Partiality { get; set; }
        public bool FabricAdvance { get; set; }
        [Display(Name = "Location")]
        [ForeignKey("LocationID")]
        public Location Location { get; set; }
        public int? LocationID { get; set; }

    }
}
