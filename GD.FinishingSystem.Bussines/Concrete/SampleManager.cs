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
        IAsyncRepository<SampleDetail> repositorySampleDetail = null;
        public SampleManager(DbContext context)
        {
            this.repositorySample = new GenericRepository<Sample>(context);
            this.repositorySampleDetail = new GenericRepository<SampleDetail>(context);
        }

        public override async Task Add(Sample sample, int adderRef)
        {
            await repositorySample.Add(sample, adderRef);
        }

        public override async Task AddSampleDetail(SampleDetail sampleDetail, int adderRef)
        {
            await repositorySampleDetail.Add(sampleDetail, adderRef);
        }

        public override async Task Delete(Sample sample, int deleterRef)
        {
            await repositorySample.Remove(sample.SampleID, deleterRef);
        }

        public override async Task DeleteSampleDetail(SampleDetail sampleDetail, int deleterRef)
        {
            await repositorySampleDetail.Remove(sampleDetail.SampleDetailID, deleterRef);
        }

        public override async Task<SampleDetail> GetSampleDetailFromSampleDetailID(int sampleDetailID)
        {
            return await repositorySampleDetail.GetByPrimaryKey(sampleDetailID);

        }

        public override async Task<IEnumerable<SampleDetail>> GetSampleDetailList()
        {
            return await repositorySampleDetail.GetWhere(x => !x.IsDeleted);
        }

        public override async Task<IEnumerable<SampleDetail>> GetSampleDetailsFromSampleID(int sampleID)
        {
            return await repositorySampleDetail.GetWhere(x => !x.IsDeleted && x.SampleID == sampleID);
        }

        public override async Task<Sample> GetSampleFromSampleID(int sampleID)
        {
            return await repositorySample.GetByPrimaryKey(sampleID);
        }

        public override async Task<IEnumerable<Sample>> GetSampleList()
        {
            return await repositorySample.GetWhere(x => !x.IsDeleted);
        }

        public override async Task Update(Sample sample, int updaterRef)
        {
            await repositorySample.Update(sample, updaterRef);
        }

        public override async Task UpdateSampleDetail(SampleDetail sampleDetail, int updaterRef)
        {
            await repositorySampleDetail.Update(sampleDetail, updaterRef);
        }
    }
}
