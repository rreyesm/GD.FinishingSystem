using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GD.FinishingSystem.Entities.ViewModels
{
    public class VMRuloMigrationReport
    {
        [Display(Name = "Rulo Migration ID")]
        public int RuloMigrationID { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }
        public string Style { get; set; }
        [Display(Name = "Style Name")]
        public string StyleName { get; set; }
        [Display(Name = "Machine")]
        public string NextMachine { get; set; }
        public string Lote { get; set; }
        [Display(Name = "Stop")]
        public string BeamStop { get; set; }
        public int Beam { get; set; }
        [Display(Name = "Is Toyota Text")]
        public string IsToyotaText { get; set; }
        public int Loom { get; set; }
        [Display(Name = "Piece No")]
        public int PieceNo { get; set; }
        [Display(Name = "Betilla")]
        public string PieceBetilla { get; set; }
        public decimal Meters { get; set; }
        [Display(Name = "Sizing Meters")]
        public decimal SizingMeters { get; set; }
        [Display(Name = "Status")]
        public string MigrationCategoryID { get; set; }
        public string Observations { get; set; }
        public int WeavingShift { get; set; }
        [Display(Name = "Rulo ID")]
        public int? RuloID { get; set; }
        [Display(Name = "Is Toyota")]
        public bool IsToyota { get; set; }
        public string Origin { get; set; }
        public string WarehouseCategoryID { get; set; }
        public int? Partiality { get; set; }
        public int? PackingListID { get; set; }
    }
}
