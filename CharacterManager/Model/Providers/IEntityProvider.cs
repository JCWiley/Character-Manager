using CharacterManager.Model.Entities;
using CharacterManager.Model.Factories;
using CharacterManager.Model.RedundantTree;
using CharacterManager.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.Providers
{
    public interface IEntityProvider
    {
        public IEntityFactory EF { get; set; }

        public IDataService DS { get; set; }

        public IRTreeMember<IEntity> AddEntity(EntityTypes type, bool ishead = false);

        public void AddChild(IRTreeMember<IEntity> Parent, IRTreeMember<IEntity> Child);

        public List<IRTreeMember<IEntity>> HeadEntities();
    }
}
