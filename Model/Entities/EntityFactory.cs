using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager.Model.Entities
{
    public class EntityFactory : IEntityFactory
    {
        public IEntity CreateCharacter()
        {
            return new Character();
        }
        public IEntity CreateOrganization()
        {
            return new Organization();
        }
    }
}
