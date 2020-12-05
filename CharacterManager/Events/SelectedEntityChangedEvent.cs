using CharacterManager.Model.Entities;
using Prism.Events;

namespace CharacterManager.Events
{
    class SelectedEntityChangedEvent : PubSubEvent<IEntity>
    {
    }
}
