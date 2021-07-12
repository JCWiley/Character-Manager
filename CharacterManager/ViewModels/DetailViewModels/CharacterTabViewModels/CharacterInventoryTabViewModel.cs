using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Providers;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace CharacterManager.ViewModels.DetailViewModels.CharacterTabViewModels
{
    public class CharacterInventoryTabViewModel : BindableBase
    {
        public CharacterInventoryTabViewModel(IEntityProvider entityProvider, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            EA = eventAggregator;
            RM = regionManager;
            EP = entityProvider;

            EA.GetEvent<UIUpdateRequestEvent>().Subscribe( UIUpdateRequestExecute );
        }

        #region Variables
        private readonly IEventAggregator EA;
        private readonly IRegionManager RM;
        private readonly IEntityProvider EP;
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
        #endregion

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

        #region Event Handlers
        private void UIUpdateRequestExecute(ChangeType type)
        {
            switch (type)
            {
                case ChangeType.SelectedCharacterChanged:
                    RaisePropertyChanged( nameof( IsEntityEnabled ) );

                    RaisePropertyChanged( nameof( Char ) );
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
