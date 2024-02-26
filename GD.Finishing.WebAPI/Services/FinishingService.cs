
using GD.FinishingSystem.Entities;
using Microsoft.Extensions.Options;
using GD.FinishingSystem.Bussines.Classes;

namespace GD.Finishing.WebAPI.Services
{
    public interface IFinishingService
    {
        Task<PackingList> GetPackingList(int packingList);
        Task<RuloMigrationModel> GetRuloMigration(int ruloMigrationID);
        Task<IEnumerable<RuloMigrationModel>> GetRuloMigrationList(int packingListNo);
        Task<int> UpdatePackingListFinishing(int packingListID, List<RuloMigrationModel> ruloMigrationList, int userID);
    }

    public class FinishingService : IFinishingService
    {
        readonly AppSettings appSettings;
        FinishingSystemFactory factory;
        public FinishingService(IOptions<AppSettings> options)
        {
            appSettings = options.Value;
            factory = new FinishingSystemFactory(this.appSettings.ConnectionString);
        }
        public Task<PackingList> GetPackingList(int packingListID)
        {
            return factory.RuloMigrations.GetPackingList(packingListID);
        }

        public async Task<RuloMigrationModel> GetRuloMigration(int ruloMigrationID)
        {
            RuloMigrationModel ruloMigrationModel = null;

            var ruloMigration = await factory.RuloMigrations.GetRuloMigration(ruloMigrationID);
            if (ruloMigration != null)
            {
                ruloMigrationModel = new RuloMigrationModel();
                ruloMigrationModel.ToRuloMugrationModel(ruloMigration);
            }

            return ruloMigrationModel;
        }

        public async Task<IEnumerable<RuloMigrationModel>> GetRuloMigrationList(int packingListID)
        {
            List<RuloMigrationModel> ruloMigrationModelList = null;
            var ruloMigrationList = await factory.RuloMigrations.GetRuloMigrationList(packingListID);

            if (ruloMigrationList != null && ruloMigrationList.Count() > 0)
            {
                ruloMigrationModelList = new List<RuloMigrationModel>();
                foreach (var item in ruloMigrationList)
                {
                    RuloMigrationModel model = new RuloMigrationModel();
                    model.ToRuloMugrationModel(item);
                    ruloMigrationModelList.Add(model);
                }
            }

            return ruloMigrationModelList;
        }

        public async Task<int> UpdatePackingListFinishing(int packingListNo, List<RuloMigrationModel> ruloMigrationList, int userID)
        {
            try
            {
                if (packingListNo == 0)
                {
                    var packingList = new PackingList();
                    packingListNo = factory.PackingList.GetNextFromSequence(PackingListType.Finishing); // packingList.PackingListID;

                    packingList.PackingListType = PackingListType.Finishing;
                    packingList.PackingListNo = packingListNo;

                    await factory.PackingList.Add(packingList, userID);

                    if (packingList != null && packingListNo == 0)
                        throw new Exception("It wasn't posible create packing list.");
                }
                else
                {
                    var packingList = await factory.PackingList.GetPackingList(packingListNo, PackingListType.Finishing);
                    await factory.PackingList.Update(packingList, userID);
                }

                foreach (var item in ruloMigrationList)
                {
                    var ruloMigration = await factory.RuloMigrations.GetRuloMigrationFromRuloMigrationID(item.RuloMigrationID);

                    if (!item.IsRejected)
                    {
                        ruloMigration.PackingListID = packingListNo;
                        ruloMigration.Observations = item.Observations;
                        ruloMigration.AccountingDate = ruloMigration.AccountingDate.GetCurrentAccountingDate();
                        ruloMigration.WarehouseCategoryID = 1; //Set to finishing area
                    }
                    else
                    {
                        ruloMigration.PackingListID = null;
                        ruloMigration.Observations = item.Observations;
                        ruloMigration.WarehouseCategoryID = 9; //9: W-1 Rejected From Finishing To Weaving
                    }

                    await factory.RuloMigrations.Update(ruloMigration, userID);
                }

                return packingListNo;
            }
            catch (Exception)
            {
                return 0;
            }

        }

    }
}
