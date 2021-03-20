using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table("tblPieces")]
    public class Piece : BaseEntity
    {
        [Key]
        public int PieceID { get; set; }
        [ForeignKey("RuloID")]
        public int RuloID { get; set; }
        public Rulo Rulo { get; set; }
        [Required, Display(Name = "Piece No.")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Please set piece number")]
        public int PieceNo { get; set; }
        public decimal Meter { get; set; }
    }
}
