using GD.FinishingSystem.Bussines.Abstract;
using GD.FinishingSystem.DAL.Abstract;
using GD.FinishingSystem.DAL.Concrete;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Concrete
{
    internal class ReprocessManager : AbstractReprocessService
    {
        IAsyncRepository<Reprocess> reprocessRepository;
        IAsyncRepository<DefinationProcess> definationProcessRepository = null;
        IAsyncRepository<OriginCategory> originRepository = null;
        IAsyncRepository<Floor> floorRepository = null;
        IAsyncRepository<WarehouseKNCategory> warehouseKNRepository = null;

        DbContext dbContext = null;

        public ReprocessManager(DbContext context)
        {
            this.reprocessRepository = new GenericRepository<Reprocess>(context);
            this.dbContext = context;
        }

        public override async Task<IEnumerable<Reprocess>> GetReprocessListFromFilters(VMRuloFilters ruloFilters)
        {
            var query = reprocessRepository.GetQueryable(x => !x.IsDeleted && ((x.Date <= ruloFilters.dtEnd && x.Date >= ruloFilters.dtBegin) || (x.Date <= ruloFilters.dtBegin && x.Date >= ruloFilters.dtEnd)));

            var definitionProcessess = await definationProcessRepository.GetWhereWithNoTrack(x => !x.IsDeleted);
            var floors = await floorRepository.GetWhereWithNoTrack(x => !x.IsDeleted);
            var mainOrigins = await originRepository.GetWhereWithNoTrack(x => !x.IsDeleted);
            var origins = mainOrigins.ToList();
            var warehouseKN = await warehouseKNRepository.GetWhereWithNoTrack(x => !x.IsDeleted);

            List<Reprocess> reprocesses = null;
            if (ruloFilters.numReprocessID > 0)
            {
                reprocesses = (from rp in query.ToList()
                               join dp in definitionProcessess on rp.DefinitionProcessID equals dp.DefinationProcessID
                               into lfDP
                               from subDP in lfDP.DefaultIfEmpty()
                               join f in floors on rp.FloorID equals f.FloorID
                               into lfF
                               from subF in lfF.DefaultIfEmpty()
                               join mo in origins on rp.FloorID equals mo.OriginCategoryID
                               into lfMO
                               from subMO in lfMO.DefaultIfEmpty()
                               join o in origins on rp.FloorID equals o.OriginCategoryID
                               into lfO
                               from subO in lfO.DefaultIfEmpty()
                               join w in warehouseKN on rp.FloorID equals w.WarehouseKNCategoryID
                               into lfW
                               from subW in lfW.DefaultIfEmpty()
                               where (rp.ReprocessID == ruloFilters.numReprocessID)
                               orderby rp.ReprocessID descending
                               select new Reprocess
                               {
                                   ReprocessID = rp.ReprocessID,
                                   WarehouseKNCategory = subW,
                                   WarehouseKNCategoryID = rp.WarehouseKNCategoryID,
                                   Date = rp.Date,
                                   PieceID = rp.PieceID,
                                   WithoutPzaID = rp.WithoutPzaID,
                                   Style = rp.Style,
                                   StyleName = rp.StyleName,
                                   Splice = rp.Splice,
                                   PpHsy = rp.PpHsy,
                                   OnzYd2 = rp.OnzYd2,
                                   Lote = rp.Lote,
                                   Beam = rp.Beam,
                                   Loom = rp.Loom,
                                   Pallet = rp.Pallet,
                                   Width = rp.Width,
                                   Meters = rp.Meters,
                                   Kg = rp.Kg,
                                   MainOrigin = subMO,
                                   MainOriginID = rp.MainOriginID,
                                   Origin = subO,
                                   OriginID = rp.OriginID,
                                   RollObs = rp.RollObs,
                                   PalletObs = rp.PalletObs,
                                   Floor = subF,
                                   FloorID = rp.FloorID,
                                   DefinationProcess = subDP,
                                   DefinitionProcessID = rp.DefinitionProcessID,
                                   OriginFinishingNumber = rp.OriginFinishingNumber,
                                   OriginPartiRef = rp.OriginFinishingNumber,
                                   PackingListNo = rp.PackingListNo,
                               }).ToList();
            }
            else
            {
                reprocesses = (from rp in query.ToList()
                               join dp in definitionProcessess on rp.DefinitionProcessID equals dp.DefinationProcessID
                               into lfDP
                               from subDP in lfDP.DefaultIfEmpty()
                               join f in floors on rp.FloorID equals f.FloorID
                               into lfF
                               from subF in lfF.DefaultIfEmpty()
                               join mo in origins on rp.FloorID equals mo.OriginCategoryID
                               into lfMO
                               from subMO in lfMO.DefaultIfEmpty()
                               join o in origins on rp.FloorID equals o.OriginCategoryID
                               into lfO
                               from subO in lfO.DefaultIfEmpty()
                               join w in warehouseKN on rp.FloorID equals w.WarehouseKNCategoryID
                               into lfW
                               from subW in lfW.DefaultIfEmpty()
                               where (!(ruloFilters.numLote > 0) || rp.Lote == ruloFilters.numLote.ToString()) &&
                               (!(ruloFilters.numBeam > 0) || rp.Beam == ruloFilters.numBeam) &&
                               (!(ruloFilters.numLoom > 0) || rp.Beam == ruloFilters.numLoom) &&
                               (string.IsNullOrEmpty(ruloFilters.txtStyle) || rp.Style.Contains(ruloFilters.txtStyle))
                               orderby rp.ReprocessID descending
                               select new Reprocess
                               {
                                   ReprocessID = rp.ReprocessID,
                                   WarehouseKNCategory = subW,
                                   WarehouseKNCategoryID = rp.WarehouseKNCategoryID,
                                   Date = rp.Date,
                                   PieceID = rp.PieceID,
                                   WithoutPzaID = rp.WithoutPzaID,
                                   Style = rp.Style,
                                   StyleName = rp.StyleName,
                                   Splice = rp.Splice,
                                   PpHsy = rp.PpHsy,
                                   OnzYd2 = rp.OnzYd2,
                                   Lote = rp.Lote,
                                   Beam = rp.Beam,
                                   Loom = rp.Loom,
                                   Pallet = rp.Pallet,
                                   Width = rp.Width,
                                   Meters = rp.Meters,
                                   Kg = rp.Kg,
                                   MainOrigin = subMO,
                                   MainOriginID = rp.MainOriginID,
                                   Origin = subO,
                                   OriginID = rp.OriginID,
                                   RollObs = rp.RollObs,
                                   PalletObs = rp.PalletObs,
                                   Floor = subF,
                                   FloorID = rp.FloorID,
                                   DefinationProcess = subDP,
                                   DefinitionProcessID = rp.DefinitionProcessID,
                                   OriginFinishingNumber = rp.OriginFinishingNumber,
                                   OriginPartiRef = rp.OriginFinishingNumber,
                                   PackingListNo = rp.PackingListNo,
                               }).ToList();
            }

            return reprocesses;
        }

        public override async Task<Reprocess> GetReprocess(int reprocessID)
        {
            var reprocess = await reprocessRepository.FirstOrDefault(x => !x.IsDeleted && x.ReprocessID == reprocessID);

            return reprocess;
        }

        public override async Task<decimal> GetTotalMetersByReprocess(string lote, int beam)
        {
            //We verify RuloID because if there is one that has an ID, it may be that it already has an advance. 
            var reprocess = await reprocessRepository.GetWhereWithNoTrack(x => x.Lote == lote && x.Beam == beam && x.RuloID == null);
            var total = reprocess.Sum(x => x.Meters);
            return total;
        }
    }
}
