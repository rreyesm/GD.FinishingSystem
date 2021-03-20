using GD.FinishingSystem.Bussines.Abstract;
using GD.FinishingSystem.DAL.Abstract;
using GD.FinishingSystem.DAL.Concrete;
using GD.FinishingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Concrete
{
    public class SampleManager : AbstractSampleService
    {
        IAsyncRepository<Sample> repositorySample = null;
        IAsyncRepository<User> repositoryUser = null;
        public SampleManager(DbContext context)
        {
            this.repositorySample = new GenericRepository<Sample>(context);
            this.repositoryUser = new GenericRepository<User>(context);
        }

        public override async Task Add(Sample sample, int adderRef)
        {
            await repositorySample.Add(sample, adderRef);
        }

        public override async Task Delete(Sample sample, int deleterRef)
        {
            await repositorySample.Remove(sample.SampleID, deleterRef);
        }

        public override async Task<Sample> GetSampleFromSampleID(int sampleID)
        {
            var res = await repositorySample.GetByPrimaryKey(sampleID);
            if (res != null)
                res.CutterUser = await repositoryUser.GetByPrimaryKey(res.CutterID);

            return res;
        }

        public override async Task<IEnumerable<Sample>> GetSamplesByRuloProcessID(int ruloProcessID)
        {
            var res = await repositorySample.GetWhere(x => !x.IsDeleted && x.RuloProcessID == ruloProcessID);
            var userList = await repositoryUser.GetWhere(x => !x.IsDeleted);

            var result = (from s in res
                          join u in userList on s.CutterID equals u.UserID
                          select new Sample
                          {
                              SampleID = s.SampleID,
                              RuloProcessID = s.RuloProcessID,
                              RuloProcess = s.RuloProcess,
                              Meter = s.Meter,
                              CutterID = s.CutterID,
                              CutterUser = u,
                              Details = s.Details
                          }).ToList();

            return result;
        }

        public override async Task<IEnumerable<Sample>> GetSampleList()
        {
            return await repositorySample.GetWhere(x => !x.IsDeleted);
        }

        public override async Task Update(Sample sample, int updaterRef)
        {
            await repositorySample.Update(sample, updaterRef);
        }

    }
}
