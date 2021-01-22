using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Factories;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.RedundantTree;
using CharacterManager.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace CharacterManager.Model.Providers
{
    public class JobEventProvider : IJobEventProvider
    {
        [Dependency]
        public IDataService DS { get; set; }

        [Dependency]
        public IJobEventFactory JEF{ get;set;}

        [Dependency]
        public IJobDirectoryProvider JDP { get; set; }

        public void AddEventToJob(IJob J, IEvent E)
        {
            if (DS.JobEventDict.ContainsKey(J.Job_ID))
            {
                DS.JobEventDict[J.Job_ID].Add(E);
            }
            else
            {
                DS.JobEventDict.Add(J.Job_ID, new List<IEvent>());
                DS.JobEventDict[J.Job_ID].Add(E);
            }
        }

        public void AddEventToJob(IJob J, string Character, string Comment, string Event_Type, string Job, int Progress_Effects)
        {
            IEvent E = JEF.CreateJobEvent(Character, Comment, Event_Type, Job, Progress_Effects);
            AddEventToJob(J, E);
        }

        public List<IEvent> GetAllEvents()
        {
            List<IEvent> E_List = new List<IEvent>();
            foreach (List<IEvent> events in DS.JobEventDict.Values)
            {
                E_List.AddRange(events);
            }

            return E_List;
        }

        public List<IEvent> GetEventsForEntity(IRTreeMember<IEntity> E)
        {
            List<IJob> J_List = JDP.GetEntitiesJobs(E);
            List<IEvent> E_List = new List<IEvent>();
            foreach (IJob job in J_List)
            {
                E_List.AddRange(GetEventsForJob(job));
            }
            return E_List;
        }

        public List<IEvent> GetEventsForJob(IJob J)
        {
            return DS.JobEventDict[J.Job_ID];
        }
    }
}
