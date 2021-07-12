using CharacterManager.Model.Entities;
using CharacterManager.Model.RedundantTree;
using Prism.Events;

namespace CharacterManager.Events
{
    class AlterEntityRelationshipsEvent : PubSubEvent<AlterEntityRelationshipContainer>
    {
    }

    class AlterEntityRelationshipContainer
    {
        public AlterEntityRelationshipContainer(RelationshipChangeType relationshipChangeType, IRTreeMember<IEntity> Parent, IRTreeMember<IEntity> Target)
        {
            RCT = relationshipChangeType;
            parent = Parent;
            target = Target;
        }
        public RelationshipChangeType RCT;
        public IRTreeMember<IEntity> parent;
        public IRTreeMember<IEntity> target;
    }

    enum RelationshipChangeType
    {
        Cut,
        Copy,
        Paste,
        DeleteLocal,
        DeleteGlobal
    }
}
