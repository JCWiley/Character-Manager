using CharacterManager.Model.Jobs;
using CharacterManager.Model.Providers;
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
    public class JobReportPopupViewModel : BindableBase, IDialogAware
    {
        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = "Job Report";
            JDP = parameters.GetValue<IJobDirectoryProvider>("JDP");

            Jobs = JDP.GetAllJobs();
        }
        public void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        #region Locals

        IJobDirectoryProvider JDP;

        #endregion


        #region Bindings
        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private List<IJob> jobs;
        public List<IJob> Jobs
        {
            get { return jobs; }
            set { SetProperty(ref jobs, value); }
        }

        #endregion

        #region Commands
        private DelegateCommand _commandclose;
        public DelegateCommand CommandClose => _commandclose ??= new DelegateCommand(CommandCloseExecute);
        #endregion
        #region Command Handlers
        private void CommandCloseExecute()
        {
            RaiseRequestClose(new DialogResult(ButtonResult.None));
        }
        #endregion
        #region Utilities

        #endregion
    }
}
