using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Factories;
using CharacterManager.Model.RedundantTree;
using CharacterManager.Model.Services;
using Prism.Events;
using System;
using System.Collections.Generic;

namespace CharacterManager.Model.Providers
{
    public class EntityProvider : IEntityProvider
    {
        public IEntityFactory EF { get; set; }

        public IDataService DS { get; set; }

        private readonly IEventAggregator EA;

        public IRTreeMember<IEntity> CurrentTargetAsCharacter { get; private set; }
        public IRTreeMember<IEntity> CurrentTargetAsOrganization { get; private set; }

        private IRTreeMember<IEntity> EntityCopyPasteBuffer;

        public EntityProvider(IEventAggregator eventAggregator, IDataService dataService, IEntityFactory entityFactory)
        {
            DS = dataService;
            EA = eventAggregator;
            EF = entityFactory;

            CurrentTargetAsOrganization = HeadEntities()[0];

            EA.GetEvent<SelectedEntityChangedEvent>().Subscribe( SelectedEntityChangedExecute );
            EA.GetEvent<AlterEntityRelationshipsEvent>().Subscribe( AlterEntityRelationshipsExecute );
        }

        public void NotifySelectedCharacterChanged()
        {
            EA.GetEvent<UIUpdateRequestEvent>().Publish( ChangeType.SelectedCharacterChanged );
        }
        public void NotifySelectedOrganizationChanged()
        {
            EA.GetEvent<UIUpdateRequestEvent>().Publish( ChangeType.SelectedOrganizationChanged );
        }
        public void NotifyEntityListChanged()
        {
            EA.GetEvent<UIUpdateRequestEvent>().Publish( ChangeType.EntityListChanged );
        }

        public IRTreeMember<IEntity> AddEntity(EntityTypes type, bool ishead = false)
        {
            if (type != EntityTypes.Organization && ishead == true)
            {
                throw new InvalidOperationException( "Characters cannot be heads of trees" );
            }

            IEntity NewEntity = null;
            switch (type)
            {
                case EntityTypes.Organization:
                    NewEntity = EF.CreateOrganization();
                    break;
                case EntityTypes.Character:
                    NewEntity = EF.CreateCharacter();
                    break;
                default:
                    break;
            }
            return DS.EntityTree.AddItem( NewEntity, ishead );
        }

        public void AddChild(IRTreeMember<IEntity> Parent, IRTreeMember<IEntity> Child)
        {
            DS.EntityTree.AddChild( Parent, Child );
        }

        public List<IRTreeMember<IEntity>> HeadEntities()
        {
            return DS.EntityTree.Heads;
        }

        public List<IRTreeMember<IEntity>> GetAllChildren(IRTreeMember<IEntity> rTreeMember)
        {
            return DS.EntityTree.Get_All_Children( rTreeMember );
        }
        public List<IRTreeMember<IEntity>> GetImmidiateChildren(IRTreeMember<IEntity> rTreeMember)
        {
            return DS.EntityTree.Get_Immidiate_Children( rTreeMember );
        }
        public IRTreeMember<IEntity> GetTreeMemberForEntity(IEntity entity)
        {
            return DS.EntityTree.GetMemberFromItem( entity );
        }
        public IRTreeMember<IEntity> GetTreeMemberForGuid(Guid G)
        {
            return DS.EntityTree.Get_Item( G );
        }

        #region EventHandlers
        private void SelectedEntityChangedExecute(IRTreeMember<IEntity> newTarget)
        {
            if (newTarget.Item is Character)
            {
                CurrentTargetAsCharacter = newTarget;
                NotifySelectedCharacterChanged();
            }
            else if (newTarget.Item is Organization)
            {
                CurrentTargetAsOrganization = newTarget;
                NotifySelectedOrganizationChanged();
                //EA.GetEvent<SelectedEntityChangedPostEvent>().Publish(EntityTypes.Organization);
            }
            else
            {
                throw new Exception( "Newtarget is not character or organization" );
            }
        }
        private void AlterEntityRelationshipsExecute(AlterEntityRelationshipContainer container)
        {
            IRTreeMember<IEntity> Parent = container.parent; //the parent of the target entity
            IRTreeMember<IEntity> Target = container.target; //the Entity the event was initiated by
            switch (container.RCT)
            {
                case RelationshipChangeType.Cut:
                    EntityCopyPasteBuffer = Target;
                    DS.EntityTree.RemoveChild( Parent, Target );
                    NotifyEntityListChanged();
                    break;
                case RelationshipChangeType.Copy:
                    EntityCopyPasteBuffer = Target;
                    break;
                case RelationshipChangeType.Paste:
                    if (Target.Item is Organization)
                    {
                        if (EntityCopyPasteBuffer is IRTreeMember<IEntity>)
                        {
                            if (EntityCopyPasteBuffer.IsHead == false)
                            {
                                DS.EntityTree.AddChild( Target, EntityCopyPasteBuffer );
                                NotifyEntityListChanged();
                            }
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException( "Paste operation is not permitted on non Organization entities." );
                    }
                    break;
                case RelationshipChangeType.DeleteLocal:
                    DS.EntityTree.RemoveChild( Parent, Target );
                    NotifyEntityListChanged();
                    break;
                case RelationshipChangeType.DeleteGlobal:
                    DS.EntityTree.RemoveItem( Target );
                    NotifyEntityListChanged();
                    break;
                default:
                    break;
            }
        }

        #endregion
    }


}
