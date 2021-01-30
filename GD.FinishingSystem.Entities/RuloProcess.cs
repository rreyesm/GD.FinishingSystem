using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table("tblRuloProcesses")]
    public class RuloProcess : BaseEntity
    {
        public RuloProcess()
        {
            IsFinished = false;
        }
        [Key]
        public int RuloProcessID { get; set; }

        [ForeignKey("RuloID")]
        public Rulo Rulo { get; set; }
        public int RuloID { get; set; }

        [ForeignKey("DefinationProcessID")]
        public DefinationProcess DefinationProcess { get; set; }
        public int DefinationProcessID { get; set; }

        public DateTime BeginningDate { get; set; }
        public DateTime? EndDate { get; set; }

        public decimal? FinishMeter { get; set; }
        public bool IsFinished { get; set; }

    }
}
