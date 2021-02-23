using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Items;
using CharacterManager.Model.Providers;
using CharacterManager.Model.RedundantTree;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CharacterManager.ViewModels.DetailViewModels.CharacterTabViewModels
{
    public class CharacterInventoryTabViewModel : BindableBase
    {
        public CharacterInventoryTabViewModel(IEntityProvider entityProvider, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            EA = eventAggregator;
            RM = regionManager;
            EP = entityProvider;

            EA.GetEvent<UIUpdateRequestEvent>().Subscribe(UIUpdateRequestExecute);
        }

        #region Variables
        private IEventAggregator EA;
        private IRegionManager RM;
        private IEntityProvider EP;
        #endregion
        #region Binding Targets
        public Character Char
        {
            get
            {
                if(EP.CurrentTargetAsCharacter != null)
                {
                    return (Character)EP.CurrentTargetAsCharacter.Item;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        public bool IsEntityEnabled
        {
            get
            {
                if(Char is Character)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #region Event Handlers
        private void UIUpdateRequestExecute(ChangeType type)
        {
            switch (type)
            {
                case ChangeType.SelectedCharacterChanged:
                    RaisePropertyChanged("IsEntityEnabled");

                    RaisePropertyChanged("Char");
                    break;
                case ChangeType.SelectedOrganizationChanged:
                    break;
                case ChangeType.JobEventListChanged:
                    break;
                case ChangeType.JobListChanged:
                    break;
                case ChangeType.DayAdvanced:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
