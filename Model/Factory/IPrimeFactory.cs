using Character_Manager.Model.Entities;
using Character_Manager.Model.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager.Model.Factory
{
    public interface IPrimeFactory<T,K>
    {
        IRTreeFactory<T> CreateIRTreeFactory();
        IEntityFactory CreateIEntityFactory();
        IJobFactory CreateIJobFactory();

    }
}
