using CharacterManager.Model.Interfaces;
using CharacterManager.Model.RedundantTree;
using CharacterManager.ViewModels.TreeViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

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
