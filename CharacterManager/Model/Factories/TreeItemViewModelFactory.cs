using CharacterManager.Model.Entities;
using CharacterManager.Model.RedundantTree;
using CharacterManager.ViewModels.TreeViewModels;
using Prism.Events;

namespace CharacterManager.Model.Factories
{
    class TreeItemViewModelFactory : ITreeItemViewModelFactory
    {
        private IEventAggregator EA;
        public TreeItemViewModelFactory(IEventAggregator eventAggregator)
        {
            EA = eventAggregator;
        }

        public CharacterTreeItemViewModel CreateCharacterViewModel(IRTreeMember<IEntity> target)
        {
            return new CharacterTreeItemViewModel(target, EA);
        }

        public OrganizationTreeItemViewModel CreateOrganizationViewModel(IRTreeMember<IEntity> target)
        {
            return new OrganizationTreeItemViewModel(target, this, EA);
        }
    }
}
