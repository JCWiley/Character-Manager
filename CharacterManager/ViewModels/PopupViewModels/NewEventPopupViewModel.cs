using CharacterManager.Model.Entities;
using CharacterManager.Model.Factories;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.RedundantTree;
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
    public class NewEventPopupViewModel : BindableBase, IDialogAware
    {
        public event Action<IDialogResult> RequestClose;

        public NewEventPopupViewModel(IJobEventFactory eventFactory)
        {
            EF = eventFactory;
        }

        private IJobEventFactory EF;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = "An Event Occured";
            J = parameters.GetValue<IJob>("Job");
            E = parameters.GetValue<IRTreeMember<IEntity>>("Entity");

            Name = E.Item.Name;
            JobSummary = J.Summary;
            proposedimpact = parameters.GetValue<int>("Impact");
            
            Notes = "";
            ImpactSelection = proposedimpact + 7;
        }

        private IJob J;
        private IRTreeMember<IEntity> E;


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
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        private string jobsummary;
        public string JobSummary
        {
            get { return jobsummary; }
            set { SetProperty(ref jobsummary, value); }
        }
        private string notes;
        public string Notes
        {
            get { return notes; }
            set { SetProperty(ref notes, value); }
        }
        private int impactselection;
        public int ImpactSelection
        {
            get { return impactselection; }
            set { SetProperty(ref impactselection, value); }
        }
        private int proposedimpact = 1;
        public string ImpactDescription
        {
            get { return EventTypeSwitch(proposedimpact); }
        }
        #endregion

        #region Commands
        private DelegateCommand _commandaccept;
        public DelegateCommand CommandAccept => _commandaccept ??= new DelegateCommand(CommandAcceptExecute);
        private DelegateCommand _commandignore;
        public DelegateCommand CommandIgnore => _commandignore ??= new DelegateCommand(CommandIgnoreExecute);
        #endregion

        #region Command Handlers
        private void CommandAcceptExecute()
        {
            RaiseRequestClose(new DialogResult(ButtonResult.OK,new DialogParameters { { "Event", EF.CreateJobEvent(Name, Notes, EventTypeSwitch(ImpactSelection - 7), JobSummary, ImpactSelection - 7) },{"Job",J},{"Entity",E } }));
        }
        private void CommandIgnoreExecute()
        {
            RaiseRequestClose(new DialogResult(ButtonResult.Ignore));
        }

        #endregion

        #region Utilities
        private string EventTypeSwitch(int I_Progress_Impact)
        {
            switch (I_Progress_Impact)
            {
                case -7:
                    return "Critical Failure";
                case -6:
                    return "Critical Failure";
                case -5:
                    return "Critical Failure";
                case -4:
                    return "Critical Failure";
                case -3:
                    return "Failure";
                case -2:
                    return "Failure";
                case -1:
                    return "Failure";
                case 0:
                    return "Failure";
                case 1:
                    return "Anomaly";
                case 2:
                    return "Success";
                case 3:
                    return "Success";
                case 4:
                    return "Critical Success";
                case 5:
                    return "Critical Success";
                case 6:
                    return "Critical Success";
                case 7:
                    return "Critical Success";
            }
            return "Anomaly";
        }
        #endregion
    }
}
