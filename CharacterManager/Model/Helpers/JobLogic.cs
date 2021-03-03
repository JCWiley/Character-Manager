using CharacterManager.Events;
using CharacterManager.Events.EventContainers;
using CharacterManager.Model.Events;
using CharacterManager.Model.Factories;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.Providers;
using CharacterManager.Model.Services;
using CharacterManager.ViewModels.Helpers;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.Helpers
{
    public class JobLogic :IJobLogic
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        IJobEventFactory JEF;
        IEntityProvider EP;
        IDialogServiceHelper DSH;
        IEventAggregator EA;

        public JobLogic(IJobEventFactory jobEventFactory, IEntityProvider entityProvider, IEventAggregator eventAggregator)
        {
            JEF = jobEventFactory;
            EP = entityProvider;
            EA = eventAggregator;

            //EA.GetEvent<JobEventOccuredEvent>().Subscribe(JobEventOccuredEventExecute);
        }

        #region Public Functions
        //advance a job a number of days, increment days since creation and roll for events on each day
        public void AdvanceJob(IJob job, int days)
        {
            job.Days_Since_Creation += days;
            if (IsCurrentlyProgressing(job) == true)
            {
                for (int i = 0; i < days; i++)
                {
                    AdvanceJobOneDay(job);
                }
            }
        }
        //advance a job a number of days, do not increment days since creation and do not roll for events
        public void ProgressJob(IJob job, int progress)
        {
            job.Progress += progress;
            CheckIfComplete(job);
        }

        #endregion

        private bool IsCurrentlyProgressing(IJob job)
        {
            return job.IsCurrentlyProgressing;
        }

        private void AdvanceJobOneDay(IJob job)
        {
            bool DidEventOccur = EventCheck(job);
            if (DidEventOccur == false)
            {
                job.Progress += 1;
            }
            CheckIfComplete(job);
        }
        private void CheckIfComplete(IJob job)
        {
            if (job.Duration - job.Progress <= 0)
            {
                EA.GetEvent<RequestJobEventEvent>().Publish(new JobEventRequestContainer(job, 0,true));
                MarkAsComplete(job);
            }
        }

        private void MarkAsComplete(IJob job)
        {
            if (job.Recurring == 1)
            {
                job.Progress = 0;
            }
            else
            {
                job.Complete = true;
                job.Progress = job.Duration;
            }
        }

        private bool EventCheck(IJob job)
        {
            bool CritSuccess = RollForEvent(job.SuccessChance);
            bool CritFailure = RollForEvent(job.FailureChance);

            if (CritSuccess == true)
            {
                EA.GetEvent<RequestJobEventEvent>().Publish(new JobEventRequestContainer(job, RandomNumber(2, 7)));
                return true;
            }
            else if (CritFailure == true)
            {
                EA.GetEvent<RequestJobEventEvent>().Publish(new JobEventRequestContainer(job, -1*RandomNumber(2, 7)));
                return true;
            }
            return false;
        }

        private bool RollForEvent(int Chance)
        {
            return RandomNumber(1, Chance + 1) == Chance;
        }

        private int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }
    }
}
