using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Model.Interfaces
{
    public interface IJobDirectoryProvider
    {
        public void AddEntityJob(IEntity parent_entity, IJob target_job);

        public void AddSubJob(IJob parent_job, IJob target_job);

        public List<IJob> GetEntitiesJobs(IEntity entity);

        public List<IJob> GetSubJobs(IJob job);
    }
}
