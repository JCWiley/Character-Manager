using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Model.Interfaces
{
    public interface IJobDirectoryProvider
    {
        public void AddBlankJobToEntity(IEntity parent_entity);

        public void AddBlankJobToJob(IJob parent_job);

        public List<IJob> GetEntitiesJobs(IEntity entity);

        public List<IJob> GetSubJobs(IJob job);

        public List<IEvent> GetEventSummaryForEntity(IEntity entity);
    }
}
