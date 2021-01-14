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
        [Dependency]
        public IEntityFactory EF{ get; set; }

        //[Dependency]
        public IDataService DS { get; set; }

        private IEventAggregator EA;

        private IRTreeMember<IEntity> currenttargetcharacter;
        private IRTreeMember<IEntity> currenttargetorganization;
        public IRTreeMember<IEntity> CurrentTargetAsCharacter
        {
            get
            {
                return currenttargetcharacter;
            }
        }
        public IRTreeMember<IEntity> CurrentTargetAsOrganization
        {
            get
            {
                return currenttargetorganization;
            }
        }

        public EntityProvider(IEventAggregator eventAggregator,IDataService dataService)
        {
            DS = dataService;
            EA = eventAggregator;

            EA.GetEvent<SelectedEntityChangedEvent>().Subscribe(SelectedEntityChangedExecute);
            
            currenttargetorganization = HeadEntities()[0];
            currenttargetcharacter = currenttargetorganization.Child_Items[0];
        }

        public IRTreeMember<IEntity> AddEntity(EntityTypes type,bool ishead = false)
        {
            IEntity NewEntity = EF.CreateCharacter();
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
                EA.GetEvent<SelectedEntityChangedPostEvent>().Publish(EntityTypes.Character);
            }
            else if(newTarget.Item is Organization)
            {
                currenttargetorganization = newTarget;
                EA.GetEvent<SelectedEntityChangedPostEvent>().Publish(EntityTypes.Organization);
            }
            else
            {
                throw new Exception("newtarget is not character or organization");
            }
        }
        #endregion
    }


}
