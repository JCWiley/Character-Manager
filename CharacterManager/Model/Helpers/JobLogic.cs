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

        IDataService DS;
        IJobEventFactory JEF;
        IEntityProvider EP;
        IDialogServiceHelper DSH;
        IEventAggregator EA;

        public JobLogic(IDataService dataService,IJobEventFactory jobEventFactory, IEntityProvider entityProvider, IDialogServiceHelper dialogServiceHelper, IEventAggregator eventAggregator)
        {
            DS = dataService;
            JEF = jobEventFactory;
            EP = entityProvider;
            DSH = dialogServiceHelper;
            EA = eventAggregator;

            EA.GetEvent<JobEventOccuredEvent>().Subscribe(JobEventOccuredEventExecute);
        }

        #region Public Functions
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

        public void ApplyEvent(IJob job, IEvent proposedevent)
        {
            if (DS.JobEventDict.ContainsKey(job.Job_ID))
            {
                DS.JobEventDict[job.Job_ID].Add(proposedevent);
            }
            else
            {
                DS.JobEventDict.Add(job.Job_ID, new List<IEvent>());
                DS.JobEventDict[job.Job_ID].Add(proposedevent);
            }

            if (proposedevent.Progress_Effects != 0)
            {
                job.Progress += proposedevent.Progress_Effects;
                CheckIfComplete(job);
            }
        }

        #endregion
        #region Handlers
        private void EventCreated(IDialogResult result)
        {
            IJob J = result.Parameters.GetValue<IJob>("Job");
            IEvent E = result.Parameters.GetValue<IEvent>("Event");

            EA.GetEvent<JobEventOccuredEvent>().Publish(new JobEventOccuredContainer(J, E));
        }

        private void JobEventOccuredEventExecute(JobEventOccuredContainer container)
        {
            ApplyEvent(container.TargetJob, container.NewEvent);
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
                MarkAsComplete(job);
            }
        }
        private void MarkAsComplete(IJob job)
        {
            string OwnerName = EP.GetTreeMemberForGuid(job.OwnerEntity).Item.Name;


            if (job.Recurring == 1)
            {
                //MainWindow.Display_Message_Box($"{Parent_Name} has completed work on recurring job {summary}", "Job Done.");

                IEvent JE = JEF.CreateJobEvent(OwnerName, "Repeatable Job Completed", "Completed", job.Summary, 0);
                ApplyEvent(job, JE);

                job.Progress = 0;
            }
            else
            {
                //MainWindow.Display_Message_Box($"{Parent_Name} has completed work on {summary}", "Job Done.");

                IEvent JE = JEF.CreateJobEvent(OwnerName, "Job Completed", "Completed", job.Summary, 0);
                ApplyEvent(job, JE);

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
                CreateJobEvent(job, RandomNumber(2,7));
                return true;
            }
            else if (CritFailure == true)
            {
                CreateJobEvent(job, -1 * RandomNumber(2, 7));
                return true;
            }
            return false;
        }
        private void CreateJobEvent(IJob job, int Effects)
        {
            DSH.ShowNewEventPopup(EventCreated, job, EP.GetTreeMemberForGuid(job.OwnerEntity),Effects);
        }

        private bool RollForEvent(int Chance)
        {
            return RandomNumber(1, Chance + 1) == Chance;
        }

        public int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }
    }
}
