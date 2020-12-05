using CharacterManager.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.Factories
{
    public interface IEntityFactory
    {
        public IEntity CreateCharacter();
        public IEntity CreateOrganization();
    }
}
