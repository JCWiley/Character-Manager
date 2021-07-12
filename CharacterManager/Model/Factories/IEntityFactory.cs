using CharacterManager.Model.Entities;

namespace CharacterManager.Model.Factories
{
    public interface IEntityFactory
    {
        public IEntity CreateCharacter();
        public IEntity CreateOrganization();
    }
}
