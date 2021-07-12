using CharacterManager.Model.Events;

namespace CharacterManager.Model.Factories
{
    public class JobEventFactory : IJobEventFactory
    {
        //takes a DayProvider as dependancy
        public IEvent CreateJobEvent(string character, string comment, string event_type, string job, int progress_effects, int event_date)
        {
            return new Event()
            {
                Character = character,
                Comment = comment,
                Event_Type = event_type,
                Job = job,
                Progress_Effects = progress_effects,
                Day = event_date
            };
        }
    }
}
