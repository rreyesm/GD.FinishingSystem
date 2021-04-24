using System;
using System.Collections.Generic;

#nullable disable

namespace GD.FinishingSystem.DAL.EFdbPlanta
{
    public partial class LotesDeProduccion
    {
        public string Lote { get; set; }
        public string CódigoTela { get; set; }
        public string NewEstilo { get; set; }
        public int UrdidoMts { get; set; }
        public DateTime? FechaProg { get; set; }
        public string Comentarios { get; set; }
    }
}
