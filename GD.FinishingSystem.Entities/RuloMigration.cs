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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm}")]
        public DateTime Date { get; set; }
        [Required]
        public string Style { get; set; }
        [Display(Name = "Style Name")]
        [Required]
        public string StyleName { get; set; }
        [Display(Name = "Machine")]
        public string NextMachine { get; set; }
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Lote no valid!")]
        public int Lote { get; set; }
        [Display(Name = "Stop")]
        public string Stop { get; set; }
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Beam no valid!")]
        public int Beam { get; set; }
        [Display(Name = "Is Toyota")]
        public string IsToyota { get; set; }
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Loom no valid!")]
        public int Loom { get; set; }
        [Display(Name = "Piece No")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Piece no valid!")]
        public int PieceNo { get; set; }
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
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Shift no valid!")]
        public int Shift { get; set; }
        public int? RuloID { get; set; }
        [ForeignKey(name: "RuloID")]
        public Rulo Rulo { get; set; }
    }
}
