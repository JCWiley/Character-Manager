using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Providers;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace CharacterManager.ViewModels.DetailViewModels.CharacterTabViewModels
{
    public class CharacterTabViewModel : BindableBase
    {
        public CharacterTabViewModel(IEntityProvider entityProvider, IEventAggregator eventAggregator, IRegionManager regionManager, IDerivedDataProvider derivedDataProvider)
        {
            EA = eventAggregator;
            RM = regionManager;
            EP = entityProvider;
            DDP = derivedDataProvider;

            EA.GetEvent<UIUpdateRequestEvent>().Subscribe( UIUpdateRequestExecute );
        }

        #region Variables
        private readonly IEventAggregator EA;
        private readonly IRegionManager RM;
        private readonly IEntityProvider EP;

        private IDerivedDataProvider ddp;
        public IDerivedDataProvider DDP
        {
            get
            {
                return ddp;
            }
            set
            {
                SetProperty( ref ddp, value );
            }
        }
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
        #endregion

        #region Commands
        private DelegateCommand _commandlocationselectionchanged;

        public DelegateCommand CommandLocationSelectionChanged
        {
            get
            {
                return _commandlocationselectionchanged ??= new DelegateCommand( CommandLocationSelectionChangedExecute );
            }
        }

        private DelegateCommand _commandraceselectionchanged;

        public DelegateCommand CommandRaceSelectionChanged
        {
            get
            {
                return _commandraceselectionchanged ??= new DelegateCommand( CommandRaceSelectionChangedExecute );
            }
        }
        #endregion
        #region Command handlers
        private void CommandLocationSelectionChangedExecute()
        {
            DDP.UpdateLocationsList();
        }
        private void CommandRaceSelectionChangedExecute()
        {
            DDP.UpdateRacesList();
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
