using CharacterManager.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.Factories
{
    public interface IJobEventFactory
    {
        public IEvent CreateJobEvent(string character, string comment, string event_type, string job, int progress_effects, int event_date);
    }
}
