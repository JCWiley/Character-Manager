using CharacterManager.Model.Entities;
using Prism.Events;

namespace CharacterManager.Events
{
    class SelectedEntityChangedPostEvent : PubSubEvent<EntityTypes>
    {
    }
}
