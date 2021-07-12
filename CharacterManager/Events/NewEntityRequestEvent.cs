using CharacterManager.Events.EventContainers;
using Prism.Events;

namespace CharacterManager.Events
{
    class NewEntityRequestEvent : PubSubEvent<NewEntityRequestContainer>
    {
    }
}
