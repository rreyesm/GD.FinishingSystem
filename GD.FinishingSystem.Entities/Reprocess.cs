using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Entities
{
    [Table(name: "tblReprocesses")]
    public class Reprocess : BaseEntity
    {
        [Key]
        public int ReprocessID { get; set; }
        [ForeignKey("WarehouseKNCategoryID")]
        [Display(Name = "Almacén")]
        public WarehouseKNCategory WarehouseKNCategory { get; set; }
        [Display(Name = "Almacén")]
        public int WarehouseKNCategoryID { get; set; }
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }
        [Display(Name = "Pza_ID")]
        public string PieceID { get; set; }
        [Display(Name = "Sin Pieza")]
        public bool WithoutPzaID { get; set; }
        [Display(Name = "Estilo")]
        public string Style { get; set; }
        [Display(Name = "Nombre")]
        public string StyleName { get; set; }
        [Display(Name = "Empalme")]
        public decimal Splice { get; set; }
        public decimal PpHsy { get; set; }
        public decimal OnzYd2 { get; set; }
        public string Lote { get; set; }
        [Display(Name = "Julio")]
        public int Beam { get; set; }
        [Display(Name = "Telar")]
        //TODO: Salón 2: 101-148, Salón 1: 149-248, Salón 3: 301-324, Salón 4: 401-462
        [RegularExpression(@"^(10[1-9]|1[1-3][0-9]|14[0-8])|(14[9]|1[5-9][0-9]|2[0-3][0-9]|24[0-8])|(30[1-9]|31[0-9]|32[0-4])|(40[1-9]|4[1-5][0-9]|46[0-2])$", ErrorMessage = "El Telar no existe")]
        public int Loom { get; set; }
        [Display(Name = "Tarima")]
        public string Pallet { get; set; }
        [Display(Name = "Ancho")]
        public decimal Width { get; set; }
        [Display(Name = "Metros")]
        public decimal Meters { get; set; }
        [Display(Name = "Kilos")]
        public decimal Kg { get; set; }
        [ForeignKey("MainOriginID")]
        [Display(Name = "Origen Pricipal")]
        public OriginCategory MainOrigin { get; set; }
        [Display(Name = "Origen Pricipal")]
        public int MainOriginID { get; set; }
        [ForeignKey("OriginID")]
        [Display(Name = "Origen")]
        public OriginCategory Origin { get; set; }
        [Display(Name = "Origen")]
        public int OriginID { get; set; }
        [Display(Name = "Obs. Rollo")]
        public string RollObs { get; set; }
        [Display(Name = "Obs. Tarima")]
        public string PalletObs { get; set; }
        [ForeignKey("FloorID")]
        [Display(Name = "Área")]
        public Floor Floor { get; set; }
        [Display(Name = "Área")]
        public int FloorID { get; set; }
        [ForeignKey("DefinitionProcessID")]
        [Display(Name = "Máquina Acabado")]
        public DefinationProcess DefinationProcess { get; set; }
        [Display(Name = "Máquina Acabado")]
        public int DefinitionProcessID { get; set; }
        [Display(Name = "Número Acabado Origen")]
        public int OriginFinishingNumber { get; set; }
        [Display(Name = "Partida Origen")]
        public int OriginPartiRef { get; set; }
        [Display(Name = "Packing List")]
        public int? PackingListNo { get; set; }
        public int? RuloID { get; set; }
        //Analizar si se agregaría otro almacen para los Reprocesos
    }
}
