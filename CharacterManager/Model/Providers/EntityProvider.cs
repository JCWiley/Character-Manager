using CharacterManager.Model.Entities;
using CharacterManager.Model.Factories;
using CharacterManager.Model.RedundantTree;
using CharacterManager.Model.Services;
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

        [Dependency]
        public IDataService DS { get; set; }

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
    }


}
