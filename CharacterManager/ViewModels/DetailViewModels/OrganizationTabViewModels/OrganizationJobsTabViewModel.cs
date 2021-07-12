using CharacterManager.Events;
using CharacterManager.Events.EventContainers;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.Providers;
using CharacterManager.Model.RedundantTree;
using CharacterManager.ViewModels.Helpers;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;

namespace CharacterManager.ViewModels.DetailViewModels.OrganizationTabViewModels
{
    public class OrganizationJobsTabViewModel : BindableBase
    {
        public OrganizationJobsTabViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IJobDirectoryProvider jobDirectoryProvider, IEntityProvider entityProvider, IDialogServiceHelper dialogServiceHelper, IJobEventProvider jobEventProvider)
        {
            EA = eventAggregator;
            RM = regionManager;
            JDP = jobDirectoryProvider;
            EP = entityProvider;
            DSH = dialogServiceHelper;
            JEP = jobEventProvider;

            EA.GetEvent<UIUpdateRequestEvent>().Subscribe( UIUpdateRequestExecute );

            //EA.GetEvent<SelectedEntityChangedPostEvent>().Subscribe(SelectedEntityChangedPostEventExecute);
        }

        #region Variables
        private readonly IEventAggregator EA;
        private readonly IRegionManager RM;
        private readonly IJobDirectoryProvider JDP;
        private readonly IEntityProvider EP;
        private readonly IDialogServiceHelper DSH;
        private readonly IJobEventProvider JEP;
        private IJob SelectedJob = null;

        #endregion

        #region Binding Targets
        public Organization Org
        {
            get
            {
                if (EP.CurrentTargetAsOrganization != null)
                {
                    return (Organization)EP.CurrentTargetAsOrganization.Item;
                }
                else
                {
                    return null;
                }
            }
        }
        public List<IRTreeMember<IEntity>> Entities
        {
            get
            {
                return EP.CurrentTargetAsOrganization.Child_Items;
            }
        }
        public List<IJob> Jobs
        {
            get
            {
                return JDP.GetEntitiesJobs( EP.CurrentTargetAsOrganization );
            }
        }
        public List<IJob> ChildJobs
        {
            get
            {
                if (SelectedJob != null)
                {
                    return JDP.GetSubJobs( SelectedJob );
                }
                else
                {
                    return new List<IJob>();
                }
            }
        }
        public List<IRTreeMember<IEntity>> TargetChildren
        {
            get
            {
                return EP.GetImmidiateChildren( EP.CurrentTargetAsOrganization );
            }
        }
        public bool IsEntityEnabled
        {
            get
            {
                if (Org is Organization)
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

        private DelegateCommand _commandaddsubtask;
        public DelegateCommand CommandAddSubtask
        {
            get
            {
                return _commandaddsubtask ??= new DelegateCommand( CommandAddSubtaskExecute );
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
            SelectedJob = JDP.AddBlankJobToEntity( EP.CurrentTargetAsOrganization );
            RaisePropertyChanged( nameof( IsJobEnabled ) );
            RaisePropertyChanged( nameof( Jobs ) );
        }

        private void CommandAddCustomEventExecute()
        {
            //trigger user prompt, pass selected job, prompt calls JEP to add new event.
            //DSH.ShowNewEventPopup(CustomEventCreated, SelectedJob ,EP.CurrentTargetAsOrganization);
            EA.GetEvent<RequestJobEventEvent>().Publish( new JobEventRequestContainer( SelectedJob, 0 ) );
            RaisePropertyChanged( nameof( Jobs ) );
        }

        private void CommandAddSubtaskExecute()
        {
            JDP.AddBlankJobToJob( SelectedJob );
            RaisePropertyChanged( nameof( ChildJobs ) );
        }

        private void CommandSelectedJobChangedExecute(object J)
        {
            SelectedJob = (IJob)J;
            RaisePropertyChanged( nameof( IsJobEnabled ) );
            RaisePropertyChanged( nameof( ChildJobs ) );
        }

        #endregion

        #region Event Handlers
        private void UIUpdateRequestExecute(ChangeType type)
        {
            switch (type)
            {
                case ChangeType.SelectedCharacterChanged:
                    break;
                case ChangeType.SelectedOrganizationChanged:
                    //SelectedJob = null;

                    RaisePropertyChanged( nameof( IsEntityEnabled ) );
                    RaisePropertyChanged( nameof( IsJobEnabled ) );

                    RaisePropertyChanged( nameof( Jobs ) );
                    RaisePropertyChanged( nameof( Entities ) );
                    RaisePropertyChanged( nameof( Org ) );
                    RaisePropertyChanged( nameof( TargetChildren ) );
                    break;
                case ChangeType.JobEventListChanged:
                    break;
                case ChangeType.JobListChanged:
                    RaisePropertyChanged( nameof( Jobs ) );
                    RaisePropertyChanged( nameof( Org ) );
                    break;
                case ChangeType.DayAdvanced:
                    RaisePropertyChanged( nameof( Jobs ) );
                    RaisePropertyChanged( nameof( Org ) );
                    break;
                default:
                    break;
            }
        }


        //private void CustomEventCreated(IDialogResult result)
        //{
        //    IJob J = result.Parameters.GetValue<IJob>("Job");
        //    IEvent E = result.Parameters.GetValue<IEvent>("Event");

        //    EA.GetEvent<RequestJobEventEvent>().Publish(new JobEventRequestContainer(J, RandomNumber(2, 7)));

        //    //EA.GetEvent<JobEventOccuredEvent>().Publish(new JobEventOccuredContainer(J, E));
        //}
        #endregion
    }
}
