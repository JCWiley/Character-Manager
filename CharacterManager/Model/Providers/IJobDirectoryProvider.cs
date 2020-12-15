using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Factories;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.Services;
using System.Collections.Generic;

namespace CharacterManager.Model.Providers
{
    public interface IJobDirectoryProvider
    {
        public IDataService DS { get; set; }

        public IJobFactory _jobFactory { get; set; }

        public void AddBlankJobToEntity(IEntity parent_entity);

        public void AddBlankJobToJob(IJob parent_job);

        public List<IJob> GetEntitiesJobs(IEntity entity);

        public List<IJob> GetSubJobs(IJob job);

        public List<IEvent> GetEventSummaryForEntity(IEntity entity);
    }
}
