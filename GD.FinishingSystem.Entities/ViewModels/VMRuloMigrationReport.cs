﻿using System;
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
        public string Style { get; set; }
        [Display(Name = "Style Name")]
        public string StyleName { get; set; }
        [Display(Name = "Machine")]
        public string NextMachine { get; set; }
        public int Lote { get; set; }
        [Display(Name = "Stop")]
        public string Stop { get; set; }
        public int Beam { get; set; }
        [Display(Name = "Is Toyota")]
        public string IsToyota { get; set; }
        public int Loom { get; set; }
        [Display(Name = "Piece No")]
        public int PieceNo { get; set; }
        [Display(Name = "Betilla")]
        public string PieceBetilla { get; set; }
        public decimal Meters { get; set; }
        [Display(Name = "Gummed Meters")]
        public decimal GummedMeters { get; set; }
        [Display(Name = "Status")]
        public string MigrationCategoryID { get; set; }
        public string Observations { get; set; }
        public int Shift { get; set; }
        [Display(Name = "Rulo ID")]
        public int? RuloID { get; set; }
    }
}