using GD.FinishingSystem.Bussines.Abstract;
using GD.FinishingSystem.DAL.Abstract;
using GD.FinishingSystem.DAL.Concrete;
using GD.FinishingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Concrete
{
    public class FloorManager : AbstractFloorService
    {
        private IAsyncRepository<Floor> repository = null;
        public FloorManager(DbContext context)
        {
            this.repository = new GenericRepository<Floor>(context);

        }
        public override async Task Add(Floor floor, int adderRef)
        {
            await repository.Add(floor, adderRef);
        }

        public override async Task Delete(Floor floor, int deleterRef)
        {
            await repository.Remove(floor.FloorID, deleterRef);
        }

        public override async Task<Floor> GetFloorFromFloorID(int FloorID)
        {
            var floor = await repository.GetByPrimaryKey(FloorID);
            return floor;
        }

        public override async Task<IEnumerable<Floor>> GetFloorList()
        {
            var floor = await repository.GetWhere(o => !o.IsDeleted);
            return floor;
        }

        public override async Task Update(Floor floor, int updaterRef)
        {
            if (floor != null && floor.FloorID > 0 && !string.IsNullOrWhiteSpace(floor.FloorName))
            {
                var newUpdate = await repository.GetByPrimaryKey(floor.FloorID);
                newUpdate.FloorName = floor.FloorName;
                await repository.Update(newUpdate, updaterRef);
            }

        }
    }
}
