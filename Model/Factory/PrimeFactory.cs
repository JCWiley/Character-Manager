using Character_Manager.Model.Entities;
using Character_Manager.Model.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager.Model.Factory
{
    class PrimeFactory<T,K> : IPrimeFactory<T, K>
    {
        public IEntityFactory CreateIEntityFactory()
        {
            return new EntityFactory();
        }

        public IJobFactory CreateIJobFactory()
        {
            return new JobFactory();
        }

        public IRTreeFactory<T> CreateIRTreeFactory()
        {
            return new RTreeFactory<T>();
        }
    }
}
