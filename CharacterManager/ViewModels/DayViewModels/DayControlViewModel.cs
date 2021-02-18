using CharacterManager.Events;
using CharacterManager.Model.Providers;
using CharacterManager.ViewModels.Helpers;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.ViewModels.DayViewModels
{
    public class DayControlViewModel :BindableBase
    {
        private IEventAggregator EA;
        private IDayProvider DP;
        private IDialogServiceHelper DSH;
        public DayControlViewModel(IEventAggregator eventAggregator, IDayProvider dayProvider, IDialogServiceHelper dialogServiceHelper)
        {
            EA = eventAggregator;
            DP = dayProvider;
            DSH = dialogServiceHelper;
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
        public DelegateCommand CommandAdvanceDay => _commandadvanceday ??= new DelegateCommand(CommandAdvanceDayExecute);
        #endregion

        #region Command Handlers
        private void CommandAdvanceDayExecute()
        {
            DSH.ShowAdvanceDayPopup(DayAdvanced, new Prism.Services.Dialogs.DialogParameters { });

            
        }
        #endregion

        private void DayAdvanced(IDialogResult result)
        {
            EA.GetEvent<AdvanceDayRequestEvent>().Publish(result.Parameters.GetValue<int>("Days"));
        }
    }
}
