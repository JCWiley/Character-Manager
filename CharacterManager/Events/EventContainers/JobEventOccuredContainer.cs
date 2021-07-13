using CharacterManager.Model.Events;
using CharacterManager.Model.Jobs;

namespace CharacterManager.Events.EventContainers
{
    struct JobEventOccuredContainer
    {
        public JobEventOccuredContainer(IJob targetjob, IEvent newevent)
        {
            TargetJob = targetjob;
            NewEvent = newevent;
        }

        public IJob TargetJob { get; set; }
        public IEvent NewEvent { get; set; }
    }
}
