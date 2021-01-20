using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.Providers
{
    interface IJobEventProvider
    {
        public List<IEvent> GetEventsForJob(IJob J);
        public List<IEvent> GetEventsForEntity(IEntity E);
        public List<IEvent> GetAllEvents();
        public void AddEventToJob(IJob J, string Character, string Comment, string Event_Type, string Job, int Progress_Effects);
        public void AddEventToEntity(IEntity J, string Character, string Comment, string Event_Type, string Job, int Progress_Effects);
    }
}
