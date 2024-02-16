using GD.FinishingSystem.Bussines.Abstract;
using GD.FinishingSystem.DAL.Abstract;
using GD.FinishingSystem.DAL.Concrete;
using GD.FinishingSystem.DAL.EFdbPlanta;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;
using GD.FinishingSystem.Bussines.Classes;

namespace GD.FinishingSystem.Bussines.Concrete
{
    public class RuloMigrationsManager : AbstractRuloMigrationService
    {
        IAsyncRepository<RuloMigration> repository = null;
        IAsyncRepository<MigrationCategory> repositoryMigrationCategory = null;
        IAsyncRepository<MigrationControl> repositoryMigrationControl = null;
        IAsyncRepository<DefinationProcess> repositoryProcess = null;
        IAsyncRepository<OriginCategory> repositoryOriginCategory = null;
        IAsyncRepository<WarehouseCategory> repositoryWarehouseCategory = null;
        IAsyncRepository<Location> repositoryLocation = null;
        IAsyncRepository<TblFinishRawFabricEntrance> repositoryFinishRawFabricEntrance = null;
        IAsyncRepository<Floor> repositoryFloor = null;
        IAsyncRepository<PackingList> repositoryPackingList = null;
        private DbContext context = null;
        public RuloMigrationsManager(DbContext context)
        {
            this.repository = new GenericRepository<RuloMigration>(context);
            this.repositoryMigrationCategory = new GenericRepository<MigrationCategory>(context);
            this.repositoryMigrationControl = new GenericRepository<MigrationControl>(context);
            this.repositoryProcess = new GenericRepository<DefinationProcess>(context);
            this.repositoryOriginCategory = new GenericRepository<OriginCategory>(context);
            this.repositoryWarehouseCategory = new GenericRepository<WarehouseCategory>(context);
            this.repositoryLocation = new GenericRepository<Location>(context);
            this.repositoryFinishRawFabricEntrance = new GenericRepository<TblFinishRawFabricEntrance>(context);
            this.repositoryFloor = new GenericRepository<Floor>(context);
            this.repositoryPackingList = new GenericRepository<PackingList>(context);
            this.context = context;
        }
        public override async Task Add(RuloMigration ruloMigration, int adderRef)
        {
            try
            {
                if (!ruloMigration.FabricAdvance)
                    ruloMigration.AccountingDate = ruloMigration.AccountingDate.GetCurrentAccountingDate();
                await repository.Add(ruloMigration, adderRef);
            }
            catch (Exception ex)
            {

            }

        }

        public override async Task AddRange(IEnumerable<RuloMigration> ruloMigrationList, int adderRef)
        {
            await repository.AddRange(ruloMigrationList, adderRef);
        }

        public override async Task AddMigrationControl(MigrationControl migrationControl, int addRef)
        {
            await repositoryMigrationControl.Add(migrationControl, addRef);
        }

        public override async Task Delete(RuloMigration ruloMigration, int deleterRef)
        {
            await repository.Remove(ruloMigration.RuloMigrationID, deleterRef);
        }

        public override async Task<IEnumerable<MigrationCategory>> GetMigrationCategoryList()
        {
            return await repositoryMigrationCategory.GetWhere(x => !x.IsDeleted);
        }

        public override async Task<RuloMigration> GetRuloMigrationFromRuloMigrationID(int ruloMigrationID)
        {
            var result = await repository.GetByPrimaryKey(ruloMigrationID);

            if (result != null)
            {
                var migrationCategory = await repositoryMigrationCategory.GetByPrimaryKey(result.MigrationCategoryID);
                result.MigrationCategory = migrationCategory;

                var definitionProcess = await repositoryProcess.GetByPrimaryKey(result.DefinitionProcessID);
                result.DefinitionProcess = definitionProcess;

                var origin = await repositoryOriginCategory.GetByPrimaryKey(result.OriginID);
                result.OriginCategory = origin;

                var location = await repositoryLocation.GetByPrimaryKey(result.LocationID);
                if (location != null)
                {
                    var floor = await repositoryFloor.GetByPrimaryKey(location.FloorID);
                    if (!location.Name.Contains(floor.FloorName))
                        location.Name = $"{location.Name} {floor.FloorName}";
                }
                result.Location = location;

                result.IsToyotaText = (result.IsToyotaText != null ? result.IsToyotaText : (result.IsToyotaMigration ? "SI" : "NO"));
            }

            return result;
        }

        public override async Task<IEnumerable<RuloMigration>> GetRuloMigrationList()
        {
            return await repository.GetWhere(x => !x.IsDeleted);
        }

        public override async Task<IEnumerable<RuloMigration>> GetRuloMigrationListFromBetweenDates(DateTime dtBegin, DateTime dtEnd)
        {
            var ruloMigrationList = await repository.GetWhere(x => !x.IsDeleted && ((x.Date <= dtEnd && x.Date >= dtBegin) || (x.Date <= dtBegin && x.Date >= dtEnd)));
            var migrationCategoryList = await repositoryMigrationCategory.GetWhere(x => !x.IsDeleted);
            var definitionProcessList = await repositoryProcess.GetWhere(x => !x.IsDeleted);

            ruloMigrationList.ToList().ForEach(x =>
            {
                x.MigrationCategory = migrationCategoryList.Where(y => y.MigrationCategoryID == x.MigrationCategoryID).FirstOrDefault();
                x.DefinitionProcess = definitionProcessList.Where(y => y.DefinationProcessID == x.DefinitionProcessID).FirstOrDefault();
            });

            return ruloMigrationList;
        }

        public override async Task<IEnumerable<RuloMigration>> GetRuloMigrationListFromFilters(VMRuloFilters ruloFilters)
        {
            var query = repository.GetQueryable(x => !x.IsDeleted && ((x.Date <= ruloFilters.dtEnd && x.Date >= ruloFilters.dtBegin) || (x.Date <= ruloFilters.dtBegin && x.Date >= ruloFilters.dtEnd)));

            var migrationCategories = await repositoryMigrationCategory.GetWhereWithNoTrack(x => !x.IsDeleted);
            var definitionProcessess = await repositoryProcess.GetWhereWithNoTrack(x => !x.IsDeleted);
            var locations = await repositoryLocation.GetWhereWithNoTrack(x => !x.IsDeleted);
            var floors = await repositoryFloor.GetWhereWithNoTrack(x => !x.IsDeleted);
            var warehouses = await repositoryWarehouseCategory.GetWhereWithNoTrack(x => !x.IsDeleted);

            List<RuloMigration> rulosMigration = null;
            if (ruloFilters.numRuloMigrationID > 0)
            {
                rulosMigration = (from rm in query.ToList()
                                  join mc in migrationCategories on rm.MigrationCategoryID equals mc.MigrationCategoryID
                                  join dp in definitionProcessess on rm.DefinitionProcessID equals dp.DefinationProcessID
                                  into ljDP
                                  from subDP in ljDP.DefaultIfEmpty()
                                  join l in locations on rm.LocationID equals l.LocationID
                                  into ll
                                  from subL in ll.DefaultIfEmpty()
                                  join f in floors on subL?.FloorID equals f.FloorID
                                  into lf
                                  from subF in lf.DefaultIfEmpty()
                                  join wh in warehouses on rm.WarehouseCategoryID equals wh.WarehouseCategoryID
                                  into whlf
                                  from subWH in whlf.DefaultIfEmpty()
                                  where
                                  (rm.RuloMigrationID == ruloFilters.numRuloMigrationID)
                                  orderby rm.RuloMigrationID descending
                                  select new RuloMigration
                                  {
                                      RuloMigrationID = rm.RuloMigrationID,
                                      Date = rm.Date,
                                      Style = rm.Style,
                                      StyleName = rm.StyleName,
                                      NextMachine = (subDP != null ? subDP.Name : rm.NextMachine),
                                      Lote = rm.Lote,
                                      BeamStop = rm.BeamStop,
                                      Beam = rm.Beam,
                                      IsToyotaText = (rm.IsToyotaText != null ? rm.IsToyotaText : (rm.IsToyotaMigration ? "SI" : "NO")),
                                      Loom = rm.Loom,
                                      PieceNo = rm.PieceNo,
                                      PieceBetilla = rm.PieceBetilla,
                                      Meters = rm.Meters,
                                      SizingMeters = rm.SizingMeters,
                                      MigrationCategoryID = rm.MigrationCategoryID,
                                      MigrationCategory = mc,
                                      Observations = rm.Observations,
                                      WeavingShift = rm.WeavingShift,
                                      RuloID = rm.RuloID,
                                      IsTestStyle = rm.IsTestStyle,
                                      DefinitionProcess = subDP,
                                      DefinitionProcessID = rm.DefinitionProcessID,
                                      IsToyotaMigration = rm.IsToyotaMigration,
                                      OriginID = rm.OriginID,
                                      Location = subL != null ? new Location() { LocationID = subL.LocationID, Name = $"{subL.Name} {subF.FloorName}", Floor = subL.Floor } : null,
                                      LocationID = rm?.LocationID,
                                      WarehouseCategoryID = rm?.WarehouseCategoryID,
                                      WarehouseCatgory = subWH,
                                      FabricAdvance = rm.FabricAdvance,
                                      PackingListID = rm.PackingListID
                                  }
             ).ToList();
            }
            else if (ruloFilters.numPackingListID > 0)
            {
                rulosMigration = (from rm in query.ToList()
                                  join mc in migrationCategories on rm.MigrationCategoryID equals mc.MigrationCategoryID
                                  join dp in definitionProcessess on rm.DefinitionProcessID equals dp.DefinationProcessID
                                  into ljDP
                                  from subDP in ljDP.DefaultIfEmpty()
                                  join l in locations on rm.LocationID equals l.LocationID
                                  into ll
                                  from subL in ll.DefaultIfEmpty()
                                  join f in floors on subL?.FloorID equals f.FloorID
                                  into lf
                                  from subF in lf.DefaultIfEmpty()
                                  join wh in warehouses on rm.WarehouseCategoryID equals wh.WarehouseCategoryID
                                  into whlf
                                  from subWH in whlf.DefaultIfEmpty()
                                  where
                                  (rm.PackingListID == ruloFilters.numPackingListID)
                                  orderby rm.RuloMigrationID descending
                                  select new RuloMigration
                                  {
                                      RuloMigrationID = rm.RuloMigrationID,
                                      Date = rm.Date,
                                      Style = rm.Style,
                                      StyleName = rm.StyleName,
                                      NextMachine = (subDP != null ? subDP.Name : rm.NextMachine),
                                      Lote = rm.Lote,
                                      BeamStop = rm.BeamStop,
                                      Beam = rm.Beam,
                                      IsToyotaText = (rm.IsToyotaText != null ? rm.IsToyotaText : (rm.IsToyotaMigration ? "SI" : "NO")),
                                      Loom = rm.Loom,
                                      PieceNo = rm.PieceNo,
                                      PieceBetilla = rm.PieceBetilla,
                                      Meters = rm.Meters,
                                      SizingMeters = rm.SizingMeters,
                                      MigrationCategoryID = rm.MigrationCategoryID,
                                      MigrationCategory = mc,
                                      Observations = rm.Observations,
                                      WeavingShift = rm.WeavingShift,
                                      RuloID = rm.RuloID,
                                      IsTestStyle = rm.IsTestStyle,
                                      DefinitionProcess = subDP,
                                      DefinitionProcessID = rm.DefinitionProcessID,
                                      IsToyotaMigration = rm.IsToyotaMigration,
                                      OriginID = rm.OriginID,
                                      Location = subL != null ? new Location() { LocationID = subL.LocationID, Name = $"{subL.Name} {subF.FloorName}", Floor = subL.Floor } : null,
                                      LocationID = rm?.LocationID,
                                      WarehouseCategoryID = rm?.WarehouseCategoryID,
                                      WarehouseCatgory = subWH,
                                      FabricAdvance = rm.FabricAdvance,
                                      PackingListID = rm.PackingListID
                                  }
            ).ToList();
            }
            else
            {

                rulosMigration = (from rm in query.ToList()
                                  join mc in migrationCategories on rm.MigrationCategoryID equals mc.MigrationCategoryID
                                  join dp in definitionProcessess on rm.DefinitionProcessID equals dp.DefinationProcessID
                                  into ljDP
                                  from subDP in ljDP.DefaultIfEmpty()
                                  join l in locations on rm.LocationID equals l.LocationID
                                  into ll
                                  from subL in ll.DefaultIfEmpty()
                                  join f in floors on subL?.FloorID equals f.FloorID
                                  into lf
                                  from subF in lf.DefaultIfEmpty()
                                  join wh in warehouses on rm.WarehouseCategoryID equals wh.WarehouseCategoryID
                                  into whlf
                                  from subWH in whlf.DefaultIfEmpty()
                                  where
                                  (!(ruloFilters.numLote > 0) || rm.Lote == ruloFilters.numLote.ToString()) &&
                                  (!(ruloFilters.numBeam > 0) || rm.Beam == ruloFilters.numBeam) &&
                                  (!(ruloFilters.numLoom > 0) || rm.Loom == ruloFilters.numLoom) &&
                                  ((string.IsNullOrWhiteSpace(ruloFilters.txtStyle) || rm.Style.Contains(ruloFilters.txtStyle))) &&
                                  (!(ruloFilters.numMigrationCategory > 0) || rm.MigrationCategoryID == ruloFilters.numMigrationCategory) &&
                                  (string.IsNullOrWhiteSpace(ruloFilters.txtLocation) || (subL != null && subL.Name.Equals(ruloFilters.txtLocation, StringComparison.InvariantCultureIgnoreCase)))
                                  orderby rm.RuloMigrationID descending
                                  select new RuloMigration
                                  {
                                      RuloMigrationID = rm.RuloMigrationID,
                                      Date = rm.Date,
                                      Style = rm.Style,
                                      StyleName = rm.StyleName,
                                      NextMachine = (subDP != null ? subDP.Name : rm.NextMachine),
                                      Lote = rm.Lote,
                                      BeamStop = rm.BeamStop,
                                      Beam = rm.Beam,
                                      IsToyotaText = (rm.IsToyotaText != null ? rm.IsToyotaText : (rm.IsToyotaMigration ? "SI" : "NO")),
                                      Loom = rm.Loom,
                                      PieceNo = rm.PieceNo,
                                      PieceBetilla = rm.PieceBetilla,
                                      Meters = rm.Meters,
                                      SizingMeters = rm.SizingMeters,
                                      MigrationCategoryID = rm.MigrationCategoryID,
                                      MigrationCategory = mc,
                                      Observations = rm.Observations,
                                      WeavingShift = rm.WeavingShift,
                                      RuloID = rm.RuloID,
                                      IsTestStyle = rm.IsTestStyle,
                                      DefinitionProcess = subDP,
                                      DefinitionProcessID = rm.DefinitionProcessID,
                                      IsToyotaMigration = rm.IsToyotaMigration,
                                      OriginID = rm.OriginID,
                                      Location = subL != null ? new Location() { LocationID = subL.LocationID, Name = $"{subL.Name} {subF.FloorName}", Floor = subL.Floor } : null,
                                      LocationID = rm?.LocationID,
                                      WarehouseCategoryID = rm?.WarehouseCategoryID,
                                      WarehouseCatgory = subWH,
                                      FabricAdvance = rm.FabricAdvance,
                                      PackingListID = rm.PackingListID
                                  }
                             ).ToList();
            }

            return rulosMigration;
        }

        public override async Task UpdateMigrationControls(MigrationControl migrationContol, int addRef)
        {
            await repositoryMigrationControl.Update(migrationContol, addRef);
        }

        public override async Task Update(RuloMigration ruloMigration, int updaterRef)
        {
            if (ruloMigration.RuloMigrationID != 0)
            {
                var temp = await repository.GetByPrimaryKey(ruloMigration.RuloMigrationID);
                if (temp != null && (temp.WarehouseCategoryID == 8 || temp.WarehouseCategoryID == 9) && ruloMigration.WarehouseCategoryID == 1) //Cambiar Fecha cuando cambia de 8=W1 y 9=w2 - Tejido a 1=W1 - Acabado
                    ruloMigration.AccountingDate = ruloMigration.AccountingDate.GetCurrentAccountingDate();
            }

            await repository.Update(ruloMigration, updaterRef);
        }

        public override async Task<int> CountByFileName(string fileName)
        {
            return await repository.CountWhere(x => !x.IsDeleted && x.ExcelFileName.Equals(fileName));
        }

        public override async Task<IEnumerable<String>> GetRuloMigrationStyleList()
        {
            var result = await repository.GetWhere(x => !x.IsDeleted);
            var styleList = result.Select(x => x.Style).Distinct();

            return styleList;
        }

        public override async Task<IEnumerable<VMRuloMigrationReport>> GetRawFabricStocktFromFilters(VMRuloFilters ruloFilters)
        {
            //var query = repository.GetQueryable(x => !x.IsDeleted && ((x.Date <= ruloFilters.dtEnd && x.Date >= ruloFilters.dtBegin) || (x.Date <= ruloFilters.dtBegin && x.Date >= ruloFilters.dtEnd)));
            //DateTime acountingDateBegin = GetAccountingDate(ruloFilters.dtBegin);
            //DateTime acountingDateEnd = GetAccountingDate(ruloFilters.dtEnd);
            //var query = repository.GetQueryable(x => !x.IsDeleted && ((x.AccountingDate <= ruloFilters.dtBegin.Date && x.AccountingDate >= ruloFilters.dtBegin.Date) || (x.AccountingDate <= ruloFilters.dtBegin.Date && x.AccountingDate >= ruloFilters.dtBegin.Date)));

            DateTime realDateBegin = ruloFilters.dtBegin.GetRealDate(false);
            DateTime realDateEnd = ruloFilters.dtEnd.GetRealDate(true);
            var query = repository.GetQueryable(x => !x.IsDeleted && ((x.CreatedDate <= realDateEnd && x.CreatedDate >= realDateBegin) || (x.CreatedDate <= realDateBegin && x.CreatedDate >= realDateEnd)));

            var migrationCategories = await repositoryMigrationCategory.GetWhere(x => !x.IsDeleted);
            var definitionProcessess = await repositoryProcess.GetWhere(x => !x.IsDeleted);
            var origins = await repositoryOriginCategory.GetWhere(x => !x.IsDeleted && x.OriginCategoryID == 1 || x.OriginCategoryID == 7); //PP00 y DES0
            var warehouseCategories = await repositoryWarehouseCategory.GetWhere(x => !x.IsDeleted);

            var rulosMigration = (from rm in query.ToList()
                                  join mc in migrationCategories on rm.MigrationCategoryID equals mc.MigrationCategoryID
                                  join dp in definitionProcessess on rm.DefinitionProcessID equals dp.DefinationProcessID
                                  into ljDP
                                  from subDP in ljDP.DefaultIfEmpty()
                                  join o in origins on rm.OriginID equals o.OriginCategoryID
                                  into ljO
                                  from subO in ljO.DefaultIfEmpty()

                                  join wh in warehouseCategories.ToList() on rm.WarehouseCategoryID equals wh.WarehouseCategoryID
                                  into ljWH
                                  from subWH in ljWH.DefaultIfEmpty()
                                  where
                                  (!(ruloFilters.numLote > 0) || rm.Lote == ruloFilters.numLote.ToString()) &&
                                  (!(ruloFilters.numBeam > 0) || rm.Beam == ruloFilters.numBeam) &&
                                  (!(ruloFilters.numLoom > 0) || rm.Loom == ruloFilters.numLoom) &&
                                  ((string.IsNullOrWhiteSpace(ruloFilters.txtStyle) || rm.Style.Contains(ruloFilters.txtStyle))) &&
                                  (!(ruloFilters.numMigrationCategory > 0) || rm.MigrationCategoryID == ruloFilters.numMigrationCategory) &&
                                  (rm.RuloID == null && (rm.WarehouseCategoryID == 1 || rm.WarehouseCategoryID == 2))
                                  select new VMRuloMigrationReport
                                  {
                                      RuloMigrationID = rm.RuloMigrationID,
                                      Date = rm.Date,
                                      CreationDate = rm.CreatedDate,
                                      Style = rm.Style,
                                      StyleName = rm.StyleName,
                                      NextMachine = (subDP != null ? subDP.Name : rm.NextMachine), //rm.NextMachine,
                                      Lote = rm.Lote,
                                      Partiality = rm.Partiality,
                                      BeamStop = rm.BeamStop,
                                      Beam = rm.Beam,
                                      IsToyotaText = (rm.IsToyotaText != null ? rm.IsToyotaText : (rm.IsToyotaMigration ? "SI" : "NO")),
                                      Loom = rm.Loom,
                                      PieceNo = rm.PieceNo,
                                      PieceBetilla = rm.PieceBetilla,
                                      Meters = rm.Meters,
                                      SizingMeters = rm.SizingMeters,
                                      MigrationCategoryID = mc.Name,
                                      Observations = rm.Observations,
                                      WeavingShift = rm.WeavingShift,
                                      RuloID = rm.RuloID,
                                      IsToyota = rm.IsToyotaMigration,
                                      Origin = subO != null ? subO?.Name : "Invalid origin",
                                      WarehouseCategoryID = subWH?.WarehouseCode,
                                      PackingListID = rm.PackingListID
                                  }
                         ).ToList();

            return rulosMigration;
        }

        public override async Task<IEnumerable<VMFinishedRawFabric>> GetFinishedProcessRawFabric(VMReportFilter reportFilter)
        {
            DateTime dynamicDateBegin = reportFilter.dtBegin;
            DateTime dynamicDateEnd = reportFilter.dtEnd;

            dynamicDateBegin = dynamicDateBegin.GetRealDate(false);
            dynamicDateEnd = dynamicDateEnd.GetRealDate(true);

            string query = string.Empty;

            //Order in stored procedure
            var txtStyle = string.IsNullOrWhiteSpace(reportFilter.txtStyle) ? reportFilter.txtStyle : null;
            var numLote = reportFilter.numLote != 0 ? (int?)reportFilter.numLote : null;
            var numBeam = reportFilter.numBeam != 0 ? (int?)reportFilter.numBeam : null;
            var numLoom = reportFilter.numLoom != 0 ? (int?)reportFilter.numLoom : null;
            var shift = reportFilter.shift != 0 ? (int?)reportFilter.shift : null;
            bool withBatches = reportFilter.withBatches;

            object[] parameters = new object[] {
                    dynamicDateBegin.ToString("yyyy-MM-dd HH:mm:ss"),
                    dynamicDateEnd.ToString("yyyy-MM-dd HH:mm:ss"),
                    txtStyle,
                    numLote,
                    numBeam,
                    numLoom,
                    shift,
                    withBatches
                    };

            var rawReportList = await context.Set<VMFinishedRawFabric>().FromSqlRaw("exec stpFinishedProcessRawFabric @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7", parameters).ToListAsync();

            return rawReportList;
        }

        public override async Task<IEnumerable<VMRuloMigrationReport>> GetAllTheInformationFromRawFabric(DateTime dtEnd)
        {
            var query = repository.GetQueryable(x => !x.IsDeleted && x.Date <= dtEnd);

            var migrationCategories = await repositoryMigrationCategory.GetWhere(x => !x.IsDeleted);
            var definitionProcessess = await repositoryProcess.GetWhere(x => !x.IsDeleted);
            var origins = await repositoryOriginCategory.GetWhere(x => !x.IsDeleted && x.OriginCategoryID == 1 || x.OriginCategoryID == 7); //PP00 y DES0
            var warehouseCategories = await repositoryWarehouseCategory.GetWhere(x => !x.IsDeleted);

            var rulosMigration = (from rm in query.ToList()
                                  join mc in migrationCategories on rm.MigrationCategoryID equals mc.MigrationCategoryID
                                  join dp in definitionProcessess on rm.DefinitionProcessID equals dp.DefinationProcessID
                                  into ljDP
                                  from subDP in ljDP.DefaultIfEmpty()
                                  join o in origins on rm.OriginID equals o.OriginCategoryID
                                  into ljO
                                  from subO in ljO.DefaultIfEmpty()

                                  join wh in warehouseCategories.ToList() on rm.WarehouseCategoryID equals wh.WarehouseCategoryID
                                  into ljWH
                                  from subWH in ljWH.DefaultIfEmpty()
                                  select new VMRuloMigrationReport
                                  {
                                      RuloMigrationID = rm.RuloMigrationID,
                                      Date = rm.Date,
                                      CreationDate = rm.CreatedDate,
                                      Style = rm.Style,
                                      StyleName = rm.StyleName,
                                      NextMachine = (subDP != null ? subDP.Name : rm.NextMachine), //rm.NextMachine,
                                      Lote = rm.Lote,
                                      Partiality = rm.Partiality,
                                      BeamStop = rm.BeamStop,
                                      Beam = rm.Beam,
                                      IsToyotaText = (rm.IsToyotaText != null ? rm.IsToyotaText : (rm.IsToyotaMigration ? "SI" : "NO")),
                                      Loom = rm.Loom,
                                      PieceNo = rm.PieceNo,
                                      PieceBetilla = rm.PieceBetilla,
                                      Meters = rm.Meters,
                                      SizingMeters = rm.SizingMeters,
                                      MigrationCategoryID = mc.Name,
                                      Observations = rm.Observations,
                                      WeavingShift = rm.WeavingShift,
                                      RuloID = rm.RuloID,
                                      IsToyota = rm.IsToyotaMigration,
                                      Origin = subO != null ? subO?.Name : "Invalid origin",
                                      WarehouseCategoryID = subWH?.WarehouseCode,
                                  }
                         ).ToList();

            return rulosMigration;
        }

        public override async Task<IEnumerable<DefinationProcess>> GetDefinitionProcessList()
        {
            return await repositoryProcess.GetWhere(x => !x.IsDeleted);
        }

        public override async Task<IEnumerable<OriginCategory>> GetOriginCategoryList()
        {
            return await repositoryOriginCategory.GetWhere(x => !x.IsDeleted);
        }

        public override async Task<IEnumerable<VMStyleData>> GetStylesFromProductionLoteList()
        {
            IEnumerable<VMStyleData> styleDataList = null;
            using (dbPlantaContext context = new dbPlantaContext())
            {
                styleDataList = await (from ftt in context.FichaTecnicaTelas
                                       join lp in context.LotesDeProduccions
                                       on ftt.CódigoTela equals lp.CódigoTela
                                       select new VMStyleData
                                       {
                                           Style = ftt.CódigoTela,
                                           StyleName = ftt.Nombre,
                                           Lote = lp.Lote
                                       }).ToListAsync();
            }

            return styleDataList;
        }

        public override async Task<bool> ExistRuloInRuloMigration(int ruloID)
        {
            var ruloMigration = await repository.CountWhere(x => x.RuloID == ruloID);

            return ruloMigration > 0;
        }

        //public override async Task<decimal> GetTotalMetersByRuloMigration(string lote, int beam, int loom, string style)
        //{
        //    //We verify RuloID because if there is one that has an ID, it may be that it already has an advance. 
        //    var ruloMigrations = await repository.GetWhereWithNoTrack(x => !x.IsDeleted && x.Lote == lote && x.Beam == beam && x.Loom == loom && x.Style == style && x.RuloID == null && x.FabricAdvance != true);
        //    var total = ruloMigrations.Sum(x => x.Meters);
        //    return total;
        //}

        public override async Task<bool> UpdateRuloMigrationsFromRuloMigrationIDs(List<int> rawsIDs, int ruloID, int userID)
        {
            bool isOk = false;
            try
            {
                var result = await repository.GetByPrimaryKey(rawsIDs.FirstOrDefault());

                //Validate if Raw is fabric test we take relationship 1-RAW:1-Rulo
                if (result.OriginID == 7) //DES0
                {
                    //var ruloMigrations = await repository.GetWhere(x => x.RuloMigrationID == ruloMigrationID && x.RuloID == null && x.FabricAdvance != true);
                    var ruloMigrations = await repository.GetWhere(x => rawsIDs.Contains(x.RuloMigrationID) && x.RuloID == null && x.FabricAdvance != true);

                    foreach (var item in ruloMigrations)
                    {
                        item.RuloID = ruloID;
                        await repository.Update(item, userID);
                    }
                }
                else if (result.OriginID != 7 && result.FabricAdvance)
                {
                    result.RuloID = ruloID;
                    await repository.Update(result, userID);
                }
                else
                {
                    //TODO: Revisar si es que está bien agregar el telar o no, porque funcionaba bien hasta que solo varió por el telar y jaló unos registros de más
                    //var ruloMigrations = await repository.GetWhere(x => x.Lote == result.Lote && x.Beam == result.Beam && x.Loom == result.Loom && x.Style == result.Style && x.RuloID == null && x.FabricAdvance != true);
                    var ruloMigrations = await repository.GetWhere(x => rawsIDs.Contains(x.RuloMigrationID) && x.RuloID == null && x.FabricAdvance != true);

                    foreach (var item in ruloMigrations)
                    {
                        item.RuloID = ruloID;
                        await repository.Update(item, userID);
                    }
                }

                isOk = true;
            }
            catch (Exception)
            {
                isOk = false;
            }

            return isOk;
        }

        public override async Task<IEnumerable<Location>> GetLocationList()
        {
            return await repositoryLocation.GetWhere(x => !x.IsDeleted);
        }

        public override async Task<bool> Exists(RuloMigration ruloMigration)
        {
            //Validamos si existe algún registro
            var count = await repository.CountWhere(x => !x.IsDeleted && x.Lote == ruloMigration.Lote && x.Beam == ruloMigration.Beam && x.Loom == ruloMigration.Loom && x.PieceNo == ruloMigration.PieceNo && x.BeamStop == ruloMigration.BeamStop);

            if (count == 0)
            {
                return false;
            }
            else
            {
                //Si existe un registro validamos si el rulo migration pasado es 0 es porque es un registro nuevo y por lo tanto se repetiría con uno ya existentes
                if (ruloMigration.RuloMigrationID == 0)
                {
                    return true;
                }
                else
                {
                    //Si no es un registro nuevo y es uno que se está modificando, validar si es el mismo registros se tomaría como que no existe, si es diferente registros entonces se estaría duplicando con uno que ya existe al modificar el registroa actual
                    var rm = await repository.GetWhereWithNoTrack(x => !x.IsDeleted && x.Lote == ruloMigration.Lote && x.Beam == ruloMigration.Beam && x.Loom == ruloMigration.Loom && x.PieceNo == ruloMigration.PieceNo && x.BeamStop == ruloMigration.BeamStop);
                    if (rm.FirstOrDefault().RuloMigrationID != ruloMigration.RuloMigrationID)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public override async Task<bool> Exists(int ruloId)
        {
            var count = await repository.CountWhere(x => x.RuloID == ruloId);

            return count > 0;
        }

        public async override Task<IEnumerable<TblFinishRawFabricEntrance>> GetFinishedRawFabricEntrance(VMReportFilter reportFilter)
        {
            string query = string.Empty;

            //Order in stored procedure
            var txtStyle = string.IsNullOrWhiteSpace(reportFilter.txtStyle) ? reportFilter.txtStyle : null;
            var numLote = reportFilter.numLote != 0 ? (int?)reportFilter.numLote : null;
            var numBeam = reportFilter.numBeam != 0 ? (int?)reportFilter.numBeam : null;
            var numLoom = reportFilter.numLoom != 0 ? (int?)reportFilter.numLoom : null;
            var shift = reportFilter.shift != 0 ? (int?)reportFilter.shift : null;

            object[] parameters = null;
            switch (reportFilter.typeReport)
            {
                case 1:
                    query = "stpFinishedRawFabricEntrance @p0,@p1,@p2,@p3,@p4,@p5,@p6"; //Anterior: spGetProcessesCompletedReport

                    parameters = new object[] {
                    reportFilter.dtBegin.ToString("yyyy-MM-dd HH:mm:ss"),
                    reportFilter.dtEnd.ToString("yyyy-MM-dd HH:mm:ss"),
                    txtStyle,
                    numLote,
                    numBeam,
                    numLoom,
                    shift
                    };

                    break;
                case 4:
                    break;
                default:
                    break;
            }

            var rawReportList = await repositoryFinishRawFabricEntrance.GetWithRawSql(query, parameters);

            return rawReportList;
        }

        public async override Task<IEnumerable<VMRuloMigrationReport>> GetFinishedRawFabricEntranceDetailed(VMReportFilter reportFilter)
        {
            string query = "stpFinishedRawFabricEntranceDetailed @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7";

            object[] parameters = GetParameters(reportFilter);

            var rawReportList = await context.Set<VMRuloMigrationReport>().FromSqlRaw(query, parameters).ToListAsync();

            return rawReportList;
        }

        private object[] GetParameters(VMReportFilter reportFilter)
        {
            //Order in stored procedure
            var txtStyle = string.IsNullOrWhiteSpace(reportFilter.txtStyle) ? reportFilter.txtStyle : null;
            var numLote = reportFilter.numLote != 0 ? (int?)reportFilter.numLote : null;
            var numBeam = reportFilter.numBeam != 0 ? (int?)reportFilter.numBeam : null;
            var numLoom = reportFilter.numLoom != 0 ? (int?)reportFilter.numLoom : null;
            var shift = reportFilter.shift != 0 ? (int?)reportFilter.shift : null;

            return new object[] {
                    reportFilter.dtBegin.ToString("yyyy-MM-dd"),
                    reportFilter.dtEnd.ToString("yyyy-MM-dd"),
                    txtStyle,
                    numLote,
                    numBeam,
                    numLoom,
                    shift,
                    reportFilter.isFinishingDetailed
            };
        }

        public async override Task<IEnumerable<Location>> GetLocations()
        {
            var locations = await repositoryLocation.GetWhereWithNoTrack(x => !x.IsDeleted);

            return locations;
        }

        public async override Task<PackingList> GetPackingList(int packingListNo)
        {
            PackingList packingList = await repositoryPackingList.GetByPrimaryKey(packingListNo);

            return packingList;
        }
        public async override Task<IEnumerable<RuloMigration>> GetRuloMigrationList(int packingListNo)
        {
            IEnumerable<RuloMigration> ruloMigrations = await repository.GetWhereWithNoTrack(x => x.PackingListID == packingListNo);

            return ruloMigrations;
        }

        public async override Task<RuloMigration> GetRuloMigration(int ruloMigrationID)
        {
            var ruloMigration = await repository.FirstOrDefault(x => !x.IsDeleted && x.RuloMigrationID == ruloMigrationID);

            return ruloMigration;
        }

        public async override Task<IEnumerable<WarehouseStock>> GetMonthlyFinishingStockReport(VMReportFilter reportFilter)
        {
            DateTime dynamicDateBegin = reportFilter.dtBegin;
            DateTime dynamicDateEnd = reportFilter.dtEnd;

            dynamicDateBegin = dynamicDateBegin.GetRealDate(false);
            dynamicDateEnd = dynamicDateEnd.GetRealDate(true);

            string query = "stpMonthlyFinishingStockReport @p0,@p1";

            object[] parameters = new object[] {
                    dynamicDateBegin.ToString("yyyy-MM-dd HH:mm:ss"),
                    dynamicDateEnd.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            var warehouseStock = await context.Set<WarehouseStock>().FromSqlRaw(query, parameters).ToListAsync();

            return warehouseStock;
        }

        public async override Task<IEnumerable<RuloMigration>> GetRuloMigrationFromIDs(List<int> ruloMigrationList)
        {
            IEnumerable<RuloMigration> ruloMigrations = await repository.GetWhereWithNoTrack(x => ruloMigrationList.Contains(x.RuloMigrationID));

            return ruloMigrations;
        }

    }
}
