using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.ViewModels.PopupViewModels
{
    public class AdvanceDayPopupViewModel : BindableBase, IDialogAware
    {
        public event Action<IDialogResult> RequestClose;

        IDialogService DS;

        public AdvanceDayPopupViewModel(IDialogService dialogService)
        {
            DS = dialogService;
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = "Advance The Day";
            Days = 1;
        }

        public void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        #region Bindings
        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private int days;
        public int Days
        {
            get { return days; }
            set { SetProperty(ref days, value); }
        }

        #endregion

        #region Commands
        private DelegateCommand _commandadvance;
        public DelegateCommand CommandAdvance => _commandadvance ??= new DelegateCommand(CommandAdvanceExecute);

        private DelegateCommand _commandcancel;
        public DelegateCommand CommandCancel => _commandcancel ??= new DelegateCommand(CommandCancelExecute);
        #endregion

        #region Command Handlers
        private void CommandAdvanceExecute()
        {
            if(CheckDaysIsValid() == true)
            {
                RaiseRequestClose(new DialogResult(ButtonResult.OK, new DialogParameters { { "Days", Days } }));
            }
        }
        private void CommandCancelExecute()
        {
            RaiseRequestClose(new DialogResult(ButtonResult.Ignore));
        }

        #endregion

        #region Utilities
        bool CheckDaysIsValid()
        {
            bool IsValid = false;
            if (Days < 100 && Days >= 0)
            {
                IsValid = true;
            }
            //else
            //{
            //    DS.ShowDialog("NotificationDialog", new DialogParameters($"Cannot advance more than 100 days"),);
            //}
            return IsValid;
        }
        #endregion
    }
}
