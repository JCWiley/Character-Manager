﻿using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Factories;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.RedundantTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CharacterManager.Model.Providers
{
    public class JobDirectoryProvider : IJobDirectoryProvider
    {
        List<IJob> Job_Dict = new List<IJob>();
        private IJobFactory _jobFactory;

        public JobDirectoryProvider(IJobFactory jobFactory)
        {
            _jobFactory = jobFactory;
        }

        public void AddBlankJobToEntity(IEntity parent_entity)
        {
            IJob J = _jobFactory.CreateJob();
            J.OwnerEntity = parent_entity.Job_ID;
            Job_Dict.Add(J);
        }
        public void AddBlankJobToJob(IJob parent_job)
        {
            IJob J = _jobFactory.CreateJob();
            J.OwnerJob = parent_job.Job_ID;
            Job_Dict.Add(J);
        }

        public List<IJob> GetEntitiesJobs(IEntity entity)
        {
            return Job_Dict.Where(J => J.OwnerEntity == entity.Job_ID).ToList() ;
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
            return (List<IJob>)Job_Dict.Where(J => J.OwnerJob == job.Job_ID);
        }
    }
}