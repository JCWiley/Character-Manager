using CharacterManager.Events;
using CharacterManager.Model.Providers;
using CharacterManager.ViewModels.Helpers;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace CharacterManager.ViewModels.DayViewModels
{
    public class DayControlViewModel : BindableBase
    {
        private readonly IEventAggregator EA;
        private readonly IDayProvider DP;
        private readonly IDialogServiceHelper DSH;
        public DayControlViewModel(IEventAggregator eventAggregator, IDayProvider dayProvider, IDialogServiceHelper dialogServiceHelper)
        {
            EA = eventAggregator;
            DP = dayProvider;
            DSH = dialogServiceHelper;

            EA.GetEvent<UIUpdateRequestEvent>().Subscribe( UIUpdateRequestExecute );
        }

        #region Binding Targets
        public int CurrentDay
        {
            get
            {
                return DP.CurrentDay;
            }
        }
        #endregion
        #region Commands
        private DelegateCommand _commandadvanceday;
        public DelegateCommand CommandAdvanceDay
        {
            get
            {
                return _commandadvanceday ??= new DelegateCommand( CommandAdvanceDayExecute );
            }
        }
        #endregion

        #region Command Handlers
        private void CommandAdvanceDayExecute()
        {
            DSH.ShowAdvanceDayPopup( DayAdvanced );


        }
        #endregion

        private void DayAdvanced(IDialogResult result)
        {
            EA.GetEvent<AdvanceDayRequestEvent>().Publish( result.Parameters.GetValue<int>( "Days" ) );
        }

        #region Event Handlers
        private void UIUpdateRequestExecute(ChangeType type)
        {
            switch (type)
            {
                case ChangeType.SelectedCharacterChanged:
                    break;
                case ChangeType.SelectedOrganizationChanged:
                    break;
                case ChangeType.JobEventListChanged:
                    break;
                case ChangeType.JobListChanged:
                    break;
                case ChangeType.DayAdvanced:
                    RaisePropertyChanged( nameof( CurrentDay ) );
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
