using CharacterManager.Events;
using CharacterManager.Events.EventContainers;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Factories;
using CharacterManager.Model.Helpers;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.RedundantTree;
using CharacterManager.Model.Services;
using CharacterManager.ViewModels.Helpers;
using Prism.Events;
using Prism.Services.Dialogs;
using System.Collections.Generic;
using Unity;

namespace CharacterManager.Model.Providers
{
    public class JobEventProvider : IJobEventProvider
    {
        [Dependency]
        public IDataService DS
        {
            get; set;
        }

        [Dependency]
        public IJobEventFactory JEF
        {
            get; set;
        }

        [Dependency]
        public IJobDirectoryProvider JDP
        {
            get; set;
        }

        public IEventAggregator EA
        {
            get; set;
        }

        readonly IDialogServiceHelper DSH;
        readonly IJobLogic JL;
        readonly IEntityProvider EP;
        readonly IDayProvider DP;

        public JobEventProvider(IEventAggregator eventAggregator, IJobLogic jobLogic, IDialogServiceHelper dialogServiceHelper, IEntityProvider entityProvider, IDayProvider dayProvider)
        {
            EA = eventAggregator;
            JL = jobLogic;
            DSH = dialogServiceHelper;
            EP = entityProvider;
            DP = dayProvider;


            EA.GetEvent<RequestJobEventEvent>().Subscribe( RequestJobEventEventExecute );
        }



        //public void AddEventToJob(IJob J, string Character, string Comment, string Event_Type, string Job, int Progress_Effects)
        //{
        //    IEvent E = JEF.CreateJobEvent(Character, Comment, Event_Type, Job, Progress_Effects);
        //    AddEventToJob(J, E);
        //}

        //public void AddEventToJob(IJob J, IEvent E)
        //{
        //    JL.ApplyEvent(J, E);
        //}

        #region Event Creation
        public void CreateJobEvent(IJob job, int Effects)
        {
            DSH.ShowNewEventPopup( EventCreated, job, EP.GetTreeMemberForGuid( job.OwnerEntity ), Effects, DP.CurrentDay );
        }

        public void AddEventToJob(IJob job, IEvent proposedevent)
        {
            if (job is not null)
            {
                //if the job already has an entry in the job dict
                if (DS.JobEventDict.ContainsKey( job.Job_ID ))
                {
                    DS.JobEventDict[job.Job_ID].Add( proposedevent );
                }
                //otherwise it must first be added to the dicitonary
                else
                {
                    DS.JobEventDict.Add( job.Job_ID, new List<IEvent>() );
                    DS.JobEventDict[job.Job_ID].Add( proposedevent );
                }
                if (proposedevent.Progress_Effects != 0)
                {
                    JL.ProgressJob( job, proposedevent.Progress_Effects );
                }
            }
        }

        public void CreateJobCompleteEvent(IJob job)
        {
            string OwnerName = EP.GetTreeMemberForGuid( job.OwnerEntity ).Item.Name;

            if (job.Recurring == true)
            {

                DSH.ShowMessage( $"{OwnerName} has completed work on recurring job {job.Summary}", "Job Repeating." );
                IEvent JE = JEF.CreateJobEvent( OwnerName, "Repeatable Job Completed", "Completed", job.Summary, 0, DP.CurrentDay );
                AddEventToJob( job, JE );
            }
            else
            {
                DSH.ShowMessage( $"{OwnerName} has completed work on {job.Summary}", "Job Done." );

                IEvent JE = JEF.CreateJobEvent( OwnerName, "Job Completed", "Completed", job.Summary, 0, DP.CurrentDay );
                AddEventToJob( job, JE );
            }
        }

        #endregion
        #region EventGetters
        public List<IEvent> GetAllEvents()
        {
            List<IEvent> E_List = new();
            foreach (List<IEvent> events in DS.JobEventDict.Values)
            {
                E_List.AddRange( events );
            }

            return E_List;
        }
        public List<IEvent> GetEventsForEntity(IRTreeMember<IEntity> E)
        {
            List<IEvent> E_List = new();
            if (E is not null)
            {
                List<IJob> J_List = JDP.GetEntitiesJobs( E );

                foreach (IJob job in J_List)
                {
                    E_List.AddRange( GetEventsForJob( job ) );
                }
                return E_List;
            }
            else
            {
                return null;
            }

        }
        public List<IEvent> GetEventsForJob(IJob J)
        {
            if (DS.JobEventDict.ContainsKey( J.Job_ID ))
            {
                return DS.JobEventDict[J.Job_ID];
            }
            else
            {
                return new List<IEvent>();
            }

        }
        #endregion

        #region Handlers
        private void EventCreated(IDialogResult result)
        {
            IJob J = result.Parameters.GetValue<IJob>( "Job" );
            IEvent E = result.Parameters.GetValue<IEvent>( "Event" );

            AddEventToJob( J, E );

            EA.GetEvent<UIUpdateRequestEvent>().Publish( ChangeType.JobEventListChanged );
            //EA.GetEvent<JobEventOccuredEvent>().Publish(new JobEventOccuredContainer(J, E));
        }

        private void RequestJobEventEventExecute(JobEventRequestContainer paramaters)
        {
            if (paramaters.Complete == true)
            {
                CreateJobCompleteEvent( paramaters.J );
            }
            else
            {
                CreateJobEvent( paramaters.J, paramaters.E );
            }
        }


        #endregion
    }
}
