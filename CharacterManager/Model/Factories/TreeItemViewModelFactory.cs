using CharacterManager.Model.Entities;
using CharacterManager.Model.RedundantTree;
using CharacterManager.ViewModels.TreeViewModels;
using Prism.Events;

namespace CharacterManager.Model.Factories
{
    public class TreeItemViewModelFactory : ITreeItemViewModelFactory
    {
        private readonly IEventAggregator EA;
        public TreeItemViewModelFactory(IEventAggregator eventAggregator)
        {
            EA = eventAggregator;
        }

        public CharacterTreeItemViewModel CreateCharacterViewModel(IRTreeMember<IEntity> parent, IRTreeMember<IEntity> target)
        {
            return new CharacterTreeItemViewModel( parent, target, EA );
        }

        public OrganizationTreeItemViewModel CreateOrganizationViewModel(IRTreeMember<IEntity> parent, IRTreeMember<IEntity> target)
        {
            return new OrganizationTreeItemViewModel( parent, target, this, EA );
        }
    }
}
