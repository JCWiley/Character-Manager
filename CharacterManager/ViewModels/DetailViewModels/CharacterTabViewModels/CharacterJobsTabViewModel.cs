using CharacterManager.Events;
using CharacterManager.Events.EventContainers;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.Providers;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;

namespace CharacterManager.ViewModels.DetailViewModels.CharacterTabViewModels
{
    public class CharacterJobsTabViewModel : BindableBase
    {
        public CharacterJobsTabViewModel(IEntityProvider entityProvider, IEventAggregator eventAggregator, IJobDirectoryProvider jobDirectoryProvider)
        {
            EA = eventAggregator;
            EP = entityProvider;
            JDP = jobDirectoryProvider;

            EA.GetEvent<UIUpdateRequestEvent>().Subscribe( UIUpdateRequestExecute );
        }

        #region Variables
        private readonly IEventAggregator EA;
        private readonly IEntityProvider EP;
        private readonly IJobDirectoryProvider JDP;
        private IJob SelectedJob = null;

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
            set
            {
                EP.CurrentTargetAsCharacter.Item = value;
                RaisePropertyChanged( nameof( Char ) );
                RaisePropertyChanged( nameof( Jobs ) );
            }
        }
        public List<IJob> Jobs
        {
            get
            {
                return JDP.GetEntitiesJobs( EP.CurrentTargetAsCharacter );
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
        public bool IsJobEnabled
        {
            get
            {
                if (SelectedJob is IJob)
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
        private DelegateCommand _commandnewblankjob;
        public DelegateCommand CommandNewBlankJob
        {
            get
            {
                return _commandnewblankjob ??= new DelegateCommand( CommandNewBlankJobExecute );
            }
        }

        private DelegateCommand _commandaddcustomevent;
        public DelegateCommand CommandAddCustomEvent
        {
            get
            {
                return _commandaddcustomevent ??= new DelegateCommand( CommandAddCustomEventExecute );
            }
        }

        private DelegateCommand<object> _commandselectedjobchanged;
        public DelegateCommand<object> CommandSelectedJobChanged
        {
            get
            {
                return _commandselectedjobchanged ??= new DelegateCommand<object>( CommandSelectedJobChangedExecute );
            }
        }
        #endregion

        #region Command Handlers
        private void CommandNewBlankJobExecute()
        {
            SelectedJob = JDP.AddBlankJobToEntity( EP.CurrentTargetAsCharacter );
            RaisePropertyChanged( nameof( IsJobEnabled ) );
            RaisePropertyChanged( nameof( Jobs ) );
        }

        private void CommandAddCustomEventExecute()
        {
            //DSH.ShowNewEventPopup(CustomEventCreated, SelectedJob,EP.CurrentTargetAsCharacter);
            EA.GetEvent<RequestJobEventEvent>().Publish( new JobEventRequestContainer( SelectedJob, 0 ) );
            RaisePropertyChanged( nameof( Jobs ) );
        }
        private void CommandSelectedJobChangedExecute(object J)
        {
            SelectedJob = (IJob)J;
            RaisePropertyChanged( nameof( IsJobEnabled ) );
        }
        #endregion

        #region Event Handlers
        private void UIUpdateRequestExecute(ChangeType type)
        {
            switch (type)
            {
                case ChangeType.SelectedCharacterChanged:
                    //SelectedJob = null;

                    RaisePropertyChanged( nameof( IsEntityEnabled ) );
                    RaisePropertyChanged( nameof( IsJobEnabled ) );

                    RaisePropertyChanged( nameof( Char ) );
                    RaisePropertyChanged( nameof( Jobs ) );
                    break;
                case ChangeType.SelectedOrganizationChanged:
                    break;
                case ChangeType.JobEventListChanged:
                    break;
                case ChangeType.JobListChanged:
                    RaisePropertyChanged( nameof( Jobs ) );
                    RaisePropertyChanged( nameof( Char ) );
                    break;
                case ChangeType.DayAdvanced:
                    RaisePropertyChanged( nameof( Jobs ) );
                    RaisePropertyChanged( nameof( Char ) );
                    break;
                default:
                    break;
            }
        }
        //private void CustomEventCreated(IDialogResult result)
        //{
        //    IJob J = result.Parameters.GetValue<IJob>("Job");
        //    IEvent E = result.Parameters.GetValue<IEvent>("Event");

        //    EA.GetEvent<JobEventOccuredEvent>().Publish(new Events.EventContainers.JobEventOccuredContainer(J, E));
        //}
        #endregion
    }
}
