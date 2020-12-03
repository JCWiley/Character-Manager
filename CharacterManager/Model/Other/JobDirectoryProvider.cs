using CharacterManager.Model.Interfaces;
using CharacterManager.Model.RedundantTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CharacterManager.Model.Other
{
    public class JobDirectoryProvider : IJobDirectoryProvider
    {
        List<IJob> Job_Dict = new List<IJob>();

        public JobDirectoryProvider()
        {
            
        }

        public void AddEntityJob(IEntity parent_entity, IJob target_job)
        {
            if(!Job_Dict.Contains(target_job))
            {
                target_job.OwnerEntity = parent_entity.Job_ID;
                Job_Dict.Add(target_job);
            }
        }
        public void AddSubJob(IJob parent_job, IJob target_job)
        {
            if (!Job_Dict.Contains(target_job))
            {
                target_job.OwnerJob = parent_job.Job_ID;
                Job_Dict.Add(target_job);
            }
        }

        public List<IJob> GetEntitiesJobs(IEntity entity)
        {
            return (List<IJob>)Job_Dict.Where(J => J.OwnerEntity == entity.Job_ID);
        }

        public List<IJob> GetSubJobs(IJob job)
        {
            return (List<IJob>)Job_Dict.Where(J => J.OwnerJob == job.Job_ID);
        }
    }
}
