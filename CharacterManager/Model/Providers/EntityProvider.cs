using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Factories;
using CharacterManager.Model.RedundantTree;
using CharacterManager.Model.Services;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace CharacterManager.Model.Providers
{
    public class EntityProvider : IEntityProvider
    {
        public IEntityFactory EF{ get; set; }

        public IDataService DS { get; set; }

        private IEventAggregator EA;

        private IRTreeMember<IEntity> currenttargetcharacter = null;
        private IRTreeMember<IEntity> currenttargetorganization = null;
        public IRTreeMember<IEntity> CurrentTargetAsCharacter
        {
            get
            {
                if(currenttargetcharacter == null)
                {
                    currenttargetcharacter = CurrentTargetAsOrganization.Child_Items[0];
                }
                return currenttargetcharacter;
            }
        }
        public IRTreeMember<IEntity> CurrentTargetAsOrganization
        {
            get
            {
                if(currenttargetorganization == null)
                {
                    currenttargetorganization = HeadEntities()[0];
                }
                return currenttargetorganization;
            }
        }

        public EntityProvider(IEventAggregator eventAggregator,IDataService dataService, IEntityFactory entityFactory)
        {
            DS = dataService;
            EA = eventAggregator;
            EF = entityFactory;

            EA.GetEvent<SelectedEntityChangedEvent>().Subscribe(SelectedEntityChangedExecute);
        }

        public void NotifySelectedCharacterChanged()
        {
            EA.GetEvent<UIUpdateRequestEvent>().Publish(ChangeType.SelectedCharacterChanged);
        }
        public void NotifySelectedOrganizationChanged()
        {
            EA.GetEvent<UIUpdateRequestEvent>().Publish(ChangeType.SelectedOrganizationChanged);
        }

        public IRTreeMember<IEntity> AddEntity(EntityTypes type,bool ishead = false)
        {
            if (type != EntityTypes.Organization && ishead == true)
            {
                throw new InvalidOperationException("Characters cannot be heads of trees");
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
            return DS.EntityTree.AddItem(NewEntity, ishead);
        }

        public void AddChild(IRTreeMember<IEntity> Parent, IRTreeMember<IEntity> Child)
        {
            DS.EntityTree.AddChild(Parent, Child);
        }

        public List<IRTreeMember<IEntity>> HeadEntities()
        {
            return DS.EntityTree.Heads;
        }

        public List<IRTreeMember<IEntity>> GetAllChildren(IRTreeMember<IEntity> rTreeMember)
        {
            return DS.EntityTree.Get_All_Children(rTreeMember);
        }
        public List<IRTreeMember<IEntity>> GetImmidiateChildren(IRTreeMember<IEntity> rTreeMember)
        {
            return DS.EntityTree.Get_Immidiate_Children(rTreeMember);
        }
        public IRTreeMember<IEntity> GetTreeMemberForEntity(IEntity entity)
        {
            return DS.EntityTree.GetMemberFromItem(entity);
        }
        public IRTreeMember<IEntity> GetTreeMemberForGuid(Guid G)
        {
            return DS.EntityTree.Get_Item(G);
        }

        #region EventHandlers
        private void SelectedEntityChangedExecute(IRTreeMember<IEntity> newTarget)
        {
            if (newTarget.Item is Character)
            {
                currenttargetcharacter = newTarget;
                NotifySelectedCharacterChanged();
            }
            else if(newTarget.Item is Organization)
            {
                currenttargetorganization = newTarget;
                NotifySelectedOrganizationChanged();
                //EA.GetEvent<SelectedEntityChangedPostEvent>().Publish(EntityTypes.Organization);
            }
            else
            {
                throw new Exception("newtarget is not character or organization");
            }
        }
        #endregion
    }


}
