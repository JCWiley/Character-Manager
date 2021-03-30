using CharacterManager.Events;
using CharacterManager.Model.Providers;
using CharacterManager.ViewModels.Helpers;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.ViewModels.MenuViewModels
{
    public class MenuViewModel
    {
        IEventAggregator EA;
        IJobEventProvider JEP;
        IDialogServiceHelper DSH;
        public MenuViewModel(IEventAggregator eventAggregator,IJobEventProvider jobEventProvider, IDialogServiceHelper dialogServiceHelper)
        {
            EA = eventAggregator;
            JEP = jobEventProvider;
            DSH = dialogServiceHelper;
        }
        #region Commands
        private DelegateCommand _new_menuitem_command;
        private DelegateCommand _open_menuitem_command;
        private DelegateCommand _save_menuitem_command;
        private DelegateCommand _save_as_menuitem_command;
        private DelegateCommand _exit_menuitem_command;
        private DelegateCommand _generate_job_report_menuitem_command;
        private DelegateCommand _generate_event_report_menuitem_command;

        public DelegateCommand New_MenuItem_Command => _new_menuitem_command ??= new DelegateCommand(New_MenuItem_Command_Execute);
        public DelegateCommand Open_MenuItem_Command => _open_menuitem_command ??= new DelegateCommand(Open_MenuItem_Command_Execute);
        public DelegateCommand Save_MenuItem_Command => _save_menuitem_command ??= new DelegateCommand(Save_MenuItem_Command_Execute);
        public DelegateCommand Save_As_MenuItem_Command => _save_as_menuitem_command ??= new DelegateCommand(Save_As_MenuItem_Command_Execute);
        public DelegateCommand Exit_MenuItem_Command => _exit_menuitem_command ??= new DelegateCommand(Exit_MenuItem_Command_Execute);
        public DelegateCommand Generate_Job_Report_MenuItem_Command => _generate_job_report_menuitem_command ??= new DelegateCommand(Generate_Job_Report_MenuItem_Command_Execute);
        public DelegateCommand Generate_Event_Report_MenuItem_Command => _generate_event_report_menuitem_command ??= new DelegateCommand(Generate_Event_Report_MenuItem_Command_Execute);
        #endregion

        #region Command Handlers
        private void New_MenuItem_Command_Execute()
        {

        }
        private void Open_MenuItem_Command_Execute()
        {
            EA.GetEvent<DataLoadRequestEvent>().Publish(LoadRequestTypes.Dialog);
        }
        private void Save_MenuItem_Command_Execute()
        {
            EA.GetEvent<DataSaveRequestEvent>().Publish(SaveRequestTypes.Save);
        }
        private void Save_As_MenuItem_Command_Execute()
        {
            EA.GetEvent<DataSaveRequestEvent>().Publish(SaveRequestTypes.SaveAs);
        }
        private void Exit_MenuItem_Command_Execute()
        {
            EA.GetEvent<ProgramExitRequestEvent>().Publish("");
        }
        private void Generate_Job_Report_MenuItem_Command_Execute()
        {

        }
        private void Generate_Event_Report_MenuItem_Command_Execute()
        {
            DSH.ShowEventReportPopup(JEP.GetAllEvents());
        }


        #endregion
    }
}