using CharacterManager.Model.Entities;

namespace CharacterManager.Model.Factories
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
