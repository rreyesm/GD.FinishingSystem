using System;
using System.Collections.Generic;

#nullable disable

namespace GD.FinishingSystem.DAL.EFdbPlanta
{
    public partial class FichaTecnicaTela
    {
        public string CódigoTela { get; set; }
        public string NewEstilo { get; set; }
        public string Nombre { get; set; }
        public string Descripción { get; set; }
        public string Composición { get; set; }
        public string Teñido { get; set; }
        public string TeñidoEspecial { get; set; }
        public string Remontado { get; set; }
        public string Acabado { get; set; }
        public DateTime? Fecha { get; set; }
        public double? PesoPorM2 { get; set; }
        public double? PesoStdMin { get; set; }
        public double? PesoStdMax { get; set; }
        public float? NeUrdiembre { get; set; }
        public int? HilosUrdiembre { get; set; }
        public string MaterialUrdiembre { get; set; }
        public string HiladoUrdiembre { get; set; }
        public float? NeUrdiembre2 { get; set; }
        public int? HilosUrdiembre2 { get; set; }
        public string MaterialUrdiembre2 { get; set; }
        public string HiladoUrdiembre2 { get; set; }
        public double Peine { get; set; }
        public int HilosPorDiente { get; set; }
        public int? AnchoOrillos { get; set; }
        public double AnchoPeine { get; set; }
        public double AnchoAcabado { get; set; }
        public string Sarga { get; set; }
        public double PasadasTelar { get; set; }
        public double PasadasAcabado { get; set; }
        public float NeTrama { get; set; }
        public float ProporcionTrama { get; set; }
        public short? Lycra { get; set; }
        public string MaterialTrama { get; set; }
        public string HiladoTrama { get; set; }
        public float? NeTrama2 { get; set; }
        public float? ProporcionTrama2 { get; set; }
        public short? Lycra2 { get; set; }
        public string MaterialTrama2 { get; set; }
        public string HiladoTrama2 { get; set; }
        public string Goma { get; set; }
        public float? PickUp { get; set; }
        public string Observaciones { get; set; }
        public double? PesoLavado { get; set; }
        public DateTime? LastModify { get; set; }
        public string ModifyUser { get; set; }
    }
}
