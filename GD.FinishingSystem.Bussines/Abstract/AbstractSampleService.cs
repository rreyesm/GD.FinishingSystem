using GD.FinishingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Abstract
{
    public abstract class AbstractSampleService
    {
        public abstract Task<IEnumerable<Sample>> GetSampleList();
        public abstract Task<Sample> GetSampleFromSampleID(int sampleID);
        public abstract Task<IEnumerable<Sample>> GetSamplesByRuloProcessID(int ruloProcessID);
        public abstract Task Add(Sample sample, int adderRef);
        public abstract Task Update(Sample sample, int updaterRef);
        public abstract Task Delete(Sample sample, int deleterRef);
    }
}
