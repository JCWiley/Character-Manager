using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.RedundantTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.Providers
{
    public interface IJobEventProvider
    {
        public List<IEvent> GetEventsForJob(IJob J);
        public List<IEvent> GetEventsForEntity(IRTreeMember<IEntity> E);
        public List<IEvent> GetAllEvents();
        public void AddEventToJob(IJob J, string Character, string Comment, string Event_Type, string Job, int Progress_Effects);
        public void AddEventToEntity(IRTreeMember<IEntity> E, string Character, string Comment, string Event_Type, string Job, int Progress_Effects);
    }
}
