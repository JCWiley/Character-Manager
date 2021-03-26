using CharacterManager.Model.Entities;
using CharacterManager.Model.RedundantTree;
using CharacterManager.ViewModels.TreeViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Model.Factories
{
    public interface ITreeItemViewModelFactory
    {
        public OrganizationTreeItemViewModel CreateOrganizationViewModel(IRTreeMember<IEntity> parent, IRTreeMember<IEntity> target);
        public CharacterTreeItemViewModel CreateCharacterViewModel(IRTreeMember<IEntity> parent, IRTreeMember<IEntity> target);
    }
}
