using CharacterManager.Model.Entities;
using CharacterManager.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Model.Factories
{
    public class EntityFactory
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
