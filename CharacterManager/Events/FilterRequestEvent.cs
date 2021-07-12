using CharacterManager.Events.EventContainers;
using Prism.Events;

namespace CharacterManager.Events
{
    class FilterRequestEvent : PubSubEvent<FilterRequestEventContainer>
    {
    }
    public enum FilterType
    {
        Name,
        Race,
        Location
    }
}
