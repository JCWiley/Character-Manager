using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Factories;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.RedundantTree;
using CharacterManager.Model.Services;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Data;
using Unity;

namespace CharacterManager.Model.Providers
{
    public class JobDirectoryProvider : IJobDirectoryProvider
    {
        [Dependency]
        public IDataService DS { get; set; }

        [Dependency]
        public IJobFactory _jobFactory{ get; set; }


        public IEventAggregator EA { get; set; }

        public JobDirectoryProvider(IEventAggregator eventAggregator)
        {
            EA = eventAggregator;
        }

        private void NotifyJobListChanged()
        {
            EA.GetEvent<UIUpdateRequestEvent>().Publish(ChangeType.JobListChanged);
        }

        public IJob AddBlankJobToEntity(IRTreeMember<IEntity> parent_entity)
        {
            IJob J = _jobFactory.CreateJob();
            J.OwnerEntity = parent_entity.Gid;
            DS.Job_List.Add(J);
            NotifyJobListChanged();
            return J;
        }
        public IJob AddBlankJobToJob(IJob parent_job)
        {
            IJob J = _jobFactory.CreateJob();
            J.OwnerJob = parent_job.Job_ID;
            DS.Job_List.Add(J);
            NotifyJobListChanged();
            return J;
        }

        public List<IJob> GetEntitiesJobs(IRTreeMember<IEntity> entity)
        {
            if(entity is not null)
            {
                return DS.Job_List.Where(J => J.OwnerEntity == entity.Gid)?.ToList();
            }
            else
            {
                return null;
            }
            
        }

        public List<IJob> GetSubJobs(IJob job)
        {
            return DS.Job_List.Where(J => J.OwnerJob == job.Job_ID)?.ToList();
        }

        public List<IJob> GetAllJobs()
        {
            return DS.Job_List;
        }
    }
}
