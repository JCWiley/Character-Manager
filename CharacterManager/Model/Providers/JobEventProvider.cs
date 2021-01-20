using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Jobs;
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

        public void AddEventToEntity(IEntity J, string Character, string Comment, string Event_Type, string Job, int Progress_Effects)
        {
            throw new NotImplementedException();
        }

        public void AddEventToJob(IJob J, string Character, string Comment, string Event_Type, string Job, int Progress_Effects)
        {
            throw new NotImplementedException();
        }

        public List<IEvent> GetAllEvents()
        {
            throw new NotImplementedException();
        }

        public List<IEvent> GetEventsForEntity(IEntity E)
        {
            throw new NotImplementedException();
        }

        public List<IEvent> GetEventsForJob(IJob J)
        {
            throw new NotImplementedException();
        }
    }
}
