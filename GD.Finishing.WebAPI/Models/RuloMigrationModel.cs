using GD.FinishingSystem.Entities;

namespace GD.Finishing.WebAPI.Models
{
    public class RuloMigrationModel
    {
        public RuloMigrationModel()
        {
        }
        public int? WarehouseCategoryID { get; set; }
        public bool IsRejected { get; set; }
        public int RuloMigrationID { get; set; }
        public string Style { get; set; }
        public string StyleName { get; set; }
        public string Lote { get; set; }
        public int Beam { get; set; }
        public int Loom { get; set; }
        public DateTime CreationDate { get; set; }
        public string? BeamStop { get; set; }
        public int PieceNo { get; set; }
        public string? IsToyota { get; set; }
        public decimal SizingMeters { get; set; }
        public decimal Meters { get; set; }
        public string? Observations { get; set; }
        public int? PackingListID { get; set; }

        //Definir al guardar el Packing List
        //public DefinitionProcessModel NextMachine { get; set; }
        //public MigrationCategoryModel Status { get; set; }
        //public int WeavingShift { get; set; }
        //public OriginCategoryModel Origin { get; set; }

        //Se puede crear una ventana alternativa para definir su ubicación
        public LocationModel? Location { get; set; }

        public void ToRuloMugrationModel(RuloMigration ruloMigration)
        {
            WarehouseCategoryID = ruloMigration.WarehouseCategoryID;
            IsRejected = ruloMigration.Observations != null && ruloMigration.Observations.Contains("#RECHAZO:") ? true : false;
            RuloMigrationID = ruloMigration.RuloMigrationID;
            Style = ruloMigration.Style;
            StyleName = ruloMigration.StyleName;
            Lote = ruloMigration.Lote;
            Beam = ruloMigration.Beam;
            Loom = ruloMigration.Loom;
            CreationDate = ruloMigration.Date;
            BeamStop = ruloMigration.BeamStop;
            PieceNo = ruloMigration.PieceNo;
            IsToyota = ruloMigration.IsToyotaMigration ? "SI" : "NO";
            SizingMeters = ruloMigration.SizingMeters;
            Meters = ruloMigration.Meters;
            Observations = ruloMigration.Observations;
            PackingListID = ruloMigration.PackingListID;
            Location = null;
        }
    }
}
