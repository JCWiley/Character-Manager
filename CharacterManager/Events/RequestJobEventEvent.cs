using CharacterManager.Events.EventContainers;
using Prism.Events;

namespace CharacterManager.Events
{
    public class RequestJobEventEvent : PubSubEvent<JobEventRequestContainer>
    {
    }
}
