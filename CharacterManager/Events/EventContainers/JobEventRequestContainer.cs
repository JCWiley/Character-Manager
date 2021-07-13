using CharacterManager.Model.Jobs;

namespace CharacterManager.Events.EventContainers
{
    public struct JobEventRequestContainer
    {
        public JobEventRequestContainer(IJob job, int Effects, bool IsCompleteEvent = false)
        {
            J = job;
            E = Effects;
            Complete = IsCompleteEvent;
        }

        public IJob J { get; set; }
        public int E { get; set; }
        public bool Complete { get; set; }
    }
}
