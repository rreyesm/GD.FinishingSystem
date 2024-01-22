using GD.FinishingSystem.Bussines.Abstract;
using GD.FinishingSystem.DAL.Abstract;
using GD.FinishingSystem.DAL.Concrete;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using System.Data.Entity;

namespace GD.FinishingSystem.Bussines.Concrete
{
    public class PackingListManager : AbstractPackingListService
    {
        IAsyncRepository<PackingList> repository;
        DbContext context;

        public PackingListManager(DbContext context)
        {
            repository = new GenericRepository<PackingList>(context);
            this.context = context;
        }
        public async override Task Add(PackingList packingList, int addRef)
        {
            await repository.Add(packingList, addRef);
        }

        public async override Task Update(PackingList packingList, int updaterRef)
        {
            await repository.Update(packingList, updaterRef);
        }

        public async override Task<PackingList> GetPackingList(int packingListID, PackingListType packingListType)
        {
            return await repository.FirstOrDefault(x => !x.IsDeleted && x.PackingListNo == (int)packingListType && x.PackingListID == packingListID);
        }

        public override int GetNextFromSequence(PackingListType packingListType)
        {
            IDbConnection connection = context.Database.GetDbConnection();
            connection.Open();
            IDbCommand cmd = connection.CreateCommand();
            if (packingListType == PackingListType.Finishing)
                cmd.CommandText = "SELECT NEXT VALUE FOR SeqPackingListFinishing";
            else if (packingListType == PackingListType.Inspection)
                cmd.CommandText = "SELECT NEXT VALUE FOR SeqPackingListFinishing";

            int value = (int)cmd.ExecuteScalar();
            connection.Close();

            return value;
        }
    }
}
