using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Factories;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.RedundantTree;
using CharacterManager.Model.Services;
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

        public void AddBlankJobToEntity(IEntity parent_entity)
        {
            IJob J = _jobFactory.CreateJob();
            J.OwnerEntity = parent_entity.Job_ID;
            DS.Job_List.Add(J);
        }
        public void AddBlankJobToJob(IJob parent_job)
        {
            IJob J = _jobFactory.CreateJob();
            J.OwnerJob = parent_job.Job_ID;
            DS.Job_List.Add(J);
        }

        public List<IJob> GetEntitiesJobs(IEntity entity)
        {
            //ListCollectionView VS = new ListCollectionView(Job_List)
            //{
            //    Filter = f => (f as Job).OwnerEntity == entity.Job_ID
            //};
            //return VS;


            return DS.Job_List.Where(J => J.OwnerEntity == entity.Job_ID).ToList();
        }

        public List<IEvent> GetEventSummaryForEntity(IEntity entity)
        {
            List<IJob> jobs = GetEntitiesJobs(entity);
            List<IEvent> Summary = new List<IEvent>();

            foreach (IJob job in jobs)
            {
                foreach (IEvent e in job.Events)
                {
                    Summary.Add(e);
                }
            }
            return Summary;
        }

        public List<IJob> GetSubJobs(IJob job)
        {
            return DS.Job_List.Where(J => J.OwnerJob == job.Job_ID).ToList();
        }
    }
}
