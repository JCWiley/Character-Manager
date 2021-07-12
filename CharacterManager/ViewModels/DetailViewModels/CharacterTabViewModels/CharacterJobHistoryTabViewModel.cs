using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Providers;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
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

            EA.GetEvent<UIUpdateRequestEvent>().Subscribe( UIUpdateRequestExecute );
        }

        #region Variables
        private readonly IEventAggregator EA;
        private readonly IRegionManager RM;
        private readonly IEntityProvider EP;
        private readonly IJobDirectoryProvider JDP;
        private readonly IJobEventProvider JEP;
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
                return JEP.GetEventsForEntity( EP.CurrentTargetAsCharacter );
            }
        }

        #endregion
        #region Event Handlers
        private void UIUpdateRequestExecute(ChangeType type)
        {
            switch (type)
            {
                case ChangeType.SelectedCharacterChanged:
                    RaisePropertyChanged( nameof( IsEntityEnabled ) );

                    RaisePropertyChanged( nameof( Char ) );
                    RaisePropertyChanged( nameof( Events_Summary ) );
                    break;
                case ChangeType.SelectedOrganizationChanged:
                    break;
                case ChangeType.JobEventListChanged:
                    RaisePropertyChanged( nameof( Events_Summary ) );
                    break;
                case ChangeType.JobListChanged:
                    break;
                case ChangeType.DayAdvanced:
                    RaisePropertyChanged( nameof( Events_Summary ) );
                    RaisePropertyChanged( nameof( Char ) );
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
