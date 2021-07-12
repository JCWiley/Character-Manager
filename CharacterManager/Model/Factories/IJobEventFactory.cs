using CharacterManager.Model.Events;

namespace CharacterManager.Model.Factories
{
    public interface IJobEventFactory
    {
        public IEvent CreateJobEvent(string character, string comment, string event_type, string job, int progress_effects, int event_date);
    }
}
