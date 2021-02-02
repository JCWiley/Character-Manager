using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Providers;
using CharacterManager.Model.RedundantTree;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;

namespace CharacterManager.ViewModels.DetailViewModels.CharacterTabViewModels
{
    public class CharacterJobHistoryTabViewModel : BindableBase
    {
        public CharacterJobHistoryTabViewModel(IEntityProvider entityProvider, IEventAggregator eventAggregator, IRegionManager regionManager, IJobDirectoryProvider jobDirectoryProvider, IJobEventProvider jobEventProvider)
        {
            EA = eventAggregator;
            RM = regionManager;
            EP = entityProvider;
            JDP = jobDirectoryProvider;
            JEP = jobEventProvider;

            EA.GetEvent<UIUpdateRequestEvent>().Subscribe(UIUpdateRequestExecute);
        }

        #region Variables
        private IEventAggregator EA;
        private IRegionManager RM;
        private IEntityProvider EP;
        private IJobDirectoryProvider JDP;
        private IJobEventProvider JEP;
        #endregion

        #region Binding Targets
        public Character Char
        {
            get
            {
                if (EP.CurrentTargetAsCharacter != null)
                {
                    return (Character)EP.CurrentTargetAsCharacter.Item;
                }
                else
                {
                    return null;
                }
            }
        }
        public bool IsEntityEnabled
        {
            get
            {
                if (Char is Character)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public List<IEvent> Events_Summary
        {
            get
            {
                return JEP.GetEventsForEntity(EP.CurrentTargetAsCharacter);
            }
        }

        #endregion
        #region Event Handlers
        private void UIUpdateRequestExecute(ChangeType type)
        {
            switch (type)
            {
                case ChangeType.SelectedCharacterChanged:
                    RaisePropertyChanged("IsEntityEnabled");

                    RaisePropertyChanged("Char");
                    RaisePropertyChanged("Events_Summary");
                    break;
                case ChangeType.SelectedOrganizationChanged:
                    break;
                case ChangeType.JobEventListChanged:
                    RaisePropertyChanged("Events_Summary");
                    break;
                case ChangeType.JobListChanged:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
