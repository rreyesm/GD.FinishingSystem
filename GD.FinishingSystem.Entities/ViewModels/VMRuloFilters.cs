using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Entities.ViewModels
{
    public class VMRuloFilters
    {
        public DateTime dtBegin { get; set; }
        public DateTime dtEnd { get; set; }
        public bool IsAccountingDate { get; set; }
        public int numLote { get; set; }
        public int numBeam { get; set; }
        public int numLoom { get; set; }
        public int numPiece { get; set; }
        public string txtStyle { get; set; }
        public int numTestCategory { get; set; }
        public string TestResult { get; set; }
        public int numDefinitionProcess { get; set; }
        public int FolioNumber { get; set; }

        //For Rulo Raw
        public int numMigrationCategory { get; set; }
        //-----------
        public bool withBatches { get; set; }
        public int numRuloID { get; set;}

    }

}


