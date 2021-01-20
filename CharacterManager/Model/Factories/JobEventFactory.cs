using CharacterManager.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.Factories
{
    public class JobEventFactory : IJobEventFactory
    {
        //takes a DayProvider as dependancy
        public IEvent CreateJobEvent(string character, string comment, string event_type, string job, int progress_effects)
        {
            return new Event()
            {
                Character = character,
                Comment = comment,
                Event_Type = event_type,
                Job = job,
                Progress_Effects = progress_effects
            }
        }
    }
}
