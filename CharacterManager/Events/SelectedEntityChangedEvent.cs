using CharacterManager.Model.Entities;
using CharacterManager.Model.RedundantTree;
using Prism.Events;

namespace CharacterManager.Events
{
    class SelectedEntityChangedEvent : PubSubEvent<IRTreeMember<IEntity>>
    {
    }
}
