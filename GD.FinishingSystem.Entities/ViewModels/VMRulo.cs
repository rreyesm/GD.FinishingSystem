﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GD.FinishingSystem.Entities.ViewModels
{
    public class VMRulo
    {
        public int RuloID { get; set; }
        public string Lote { get; set; }
        public int Beam { get; set; }
        [Display(Name = "Beam Stop")]
        public string BeamStop { get; set; }
        public int Loom { get; set; }
        [Display(Name = "Loom Letter")]
        public string LoomLetter { get; set; }
        public int Piece { get; set; }
        [Display(Name = "Piece Letter")]
        public string PieceLetter { get; set; }
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
        [Display(Name = "Can Continue")]
        public bool CanContinue { get; set; }
        public int TestCategoryID { get; set; }
        [Display(Name = "Test Result")]
        public string TestCategoryCode { get; set; }
        [Display(Name = "Origin")]
        public string OriginID { get; set; }
        public string Observations { get; set; }
        [Display(Name = "Test Result Authorizer")]
        public string TestResultAuthorizer { get; set; }
        [Display(Name = "Folio Number")]
        public int FolioNumber { get; set; }
        [Display(Name = "Delivery Date")]
        public DateTime? DeliveryDate { get; set; }

    }
}
