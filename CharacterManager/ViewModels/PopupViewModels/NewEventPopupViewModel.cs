using CharacterManager.Model.Entities;
using CharacterManager.Model.Factories;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.RedundantTree;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace CharacterManager.ViewModels.PopupViewModels
{
    public class NewEventPopupViewModel : BindableBase, IDialogAware
    {
        public event Action<IDialogResult> RequestClose;

        public NewEventPopupViewModel(IJobEventFactory eventFactory)
        {
            EF = eventFactory;
        }

        private readonly IJobEventFactory EF;

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
            J = parameters.GetValue<IJob>( "Job" );
            E = parameters.GetValue<IRTreeMember<IEntity>>( "Entity" );
            date = parameters.GetValue<int>( "Date" );
            Name = E.Item.Name;
            JobSummary = J.Summary;
            proposedimpact = parameters.GetValue<int>( "Effects" );

            Notes = "";
            ImpactSelection = proposedimpact + 7;
        }

        private IJob J;
        private IRTreeMember<IEntity> E;


        public void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke( dialogResult );
        }

        #region Bindings
        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                SetProperty( ref title, value );
            }
        }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetProperty( ref name, value );
            }
        }
        private string jobsummary;
        public string JobSummary
        {
            get
            {
                return jobsummary;
            }
            set
            {
                SetProperty( ref jobsummary, value );
            }
        }
        private string notes;
        public string Notes
        {
            get
            {
                return notes;
            }
            set
            {
                SetProperty( ref notes, value );
            }
        }
        private int impactselection;
        public int ImpactSelection
        {
            get
            {
                return impactselection;
            }
            set
            {
                SetProperty( ref impactselection, value );
            }
        }
        private int proposedimpact = 1;
        public string ImpactDescription
        {
            get
            {
                return EventTypeSwitch( proposedimpact );
            }
        }
        private int date;
        #endregion

        #region Commands
        private DelegateCommand _commandaccept;

        public DelegateCommand GetCommandAccept()
        {
            return _commandaccept ??= new DelegateCommand( CommandAcceptExecute );
        }

        private DelegateCommand _commandignore;

        public DelegateCommand GetCommandIgnore()
        {
            return _commandignore ??= new DelegateCommand( CommandIgnoreExecute );
        }
        #endregion

        #region Command Handlers
        private void CommandAcceptExecute()
        {
            RaiseRequestClose( new DialogResult( ButtonResult.OK, new DialogParameters { { "Event", EF.CreateJobEvent( Name, Notes, EventTypeSwitch( ImpactSelection - 7 ), JobSummary, ImpactSelection - 7, date ) }, { "Job", J }, { "Entity", E } } ) );
        }
        private void CommandIgnoreExecute()
        {
            RaiseRequestClose( new DialogResult( ButtonResult.Ignore ) );
        }

        #endregion

        #region Utilities
        private static string EventTypeSwitch(int I_Progress_Impact)
        {
            return I_Progress_Impact switch
            {
                -7 => "Critical Failure",
                -6 => "Critical Failure",
                -5 => "Critical Failure",
                -4 => "Critical Failure",
                -3 => "Failure",
                -2 => "Failure",
                -1 => "Failure",
                0 => "Failure",
                1 => "Anomaly",
                2 => "Success",
                3 => "Success",
                4 => "Critical Success",
                5 => "Critical Success",
                6 => "Critical Success",
                7 => "Critical Success",
                _ => throw new InvalidOperationException(),
            };
        }
        #endregion
    }
}
