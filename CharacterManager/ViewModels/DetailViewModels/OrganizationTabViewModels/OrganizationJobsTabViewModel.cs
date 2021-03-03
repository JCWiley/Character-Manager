using CharacterManager.Events;
using CharacterManager.Events.EventContainers;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.Providers;
using CharacterManager.Model.RedundantTree;
using CharacterManager.ViewModels.Helpers;
using CharacterManager.Views.PopupViews;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.ViewModels.DetailViewModels.OrganizationTabViewModels
{
    public class OrganizationJobsTabViewModel : BindableBase
    {
        public OrganizationJobsTabViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IJobDirectoryProvider jobDirectoryProvider, IEntityProvider entityProvider,IDialogServiceHelper dialogServiceHelper,IJobEventProvider jobEventProvider)
        {
            EA = eventAggregator;
            RM = regionManager;
            JDP = jobDirectoryProvider;
            EP = entityProvider;
            DSH = dialogServiceHelper;
            JEP = jobEventProvider;

            EA.GetEvent<UIUpdateRequestEvent>().Subscribe(UIUpdateRequestExecute);

            //EA.GetEvent<SelectedEntityChangedPostEvent>().Subscribe(SelectedEntityChangedPostEventExecute);
        }

        #region Variables
        private IEventAggregator EA;
        private IRegionManager RM;
        private IJobDirectoryProvider JDP;
        private IEntityProvider EP;
        private IDialogServiceHelper DSH;
        private IJobEventProvider JEP;
        private IJob SelectedJob = null;

        #endregion

        #region Binding Targets
        public Organization Org
        {
            get
            {
                if (EP.CurrentTargetAsCharacter != null)
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
                return JDP.GetEntitiesJobs(EP.CurrentTargetAsOrganization);
            }
        }
        public List<IJob> ChildJobs
        {
            get
            {
                if(SelectedJob != null)
                {
                    return JDP.GetSubJobs(SelectedJob);
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
                return EP.GetImmidiateChildren(EP.CurrentTargetAsOrganization);
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
        public DelegateCommand CommandNewBlankJob => _commandnewblankjob ??= new DelegateCommand(CommandNewBlankJobExecute);

        private DelegateCommand _commandaddcustomevent;
        public DelegateCommand CommandAddCustomEvent => _commandaddcustomevent ??= new DelegateCommand(CommandAddCustomEventExecute);

        private DelegateCommand _commandaddsubtask;
        public DelegateCommand CommandAddSubtask => _commandaddsubtask ??= new DelegateCommand(CommandAddSubtaskExecute);

        private DelegateCommand<object> _commandselectedjobchanged;
        public DelegateCommand<object> CommandSelectedJobChanged => _commandselectedjobchanged ??= new DelegateCommand<object>(CommandSelectedJobChangedExecute);
        #endregion

        #region Command Handlers
        private void CommandNewBlankJobExecute()
        {
            SelectedJob = JDP.AddBlankJobToEntity(EP.CurrentTargetAsOrganization);
            RaisePropertyChanged("IsJobEnabled");
            RaisePropertyChanged("Jobs");
        }

        private void CommandAddCustomEventExecute()
        {
            //trigger user prompt, pass selected job, prompt calls JEP to add new event.
            //DSH.ShowNewEventPopup(CustomEventCreated, SelectedJob ,EP.CurrentTargetAsOrganization);
            EA.GetEvent<RequestJobEventEvent>().Publish(new JobEventRequestContainer(SelectedJob, 0));
            RaisePropertyChanged("Jobs");
        }

        private void CommandAddSubtaskExecute()
        {
            JDP.AddBlankJobToJob(SelectedJob);
            RaisePropertyChanged("ChildJobs");
        }

        private void CommandSelectedJobChangedExecute(object J)
        {
            SelectedJob = (IJob)J;
            RaisePropertyChanged("IsJobEnabled");
            RaisePropertyChanged("ChildJobs");
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

                    RaisePropertyChanged("IsEntityEnabled");
                    RaisePropertyChanged("IsJobEnabled");

                    RaisePropertyChanged("Jobs");
                    RaisePropertyChanged("Entities");
                    RaisePropertyChanged("Org");
                    RaisePropertyChanged("TargetChildren");
                    break;
                case ChangeType.JobEventListChanged:
                    break;
                case ChangeType.JobListChanged:
                    RaisePropertyChanged("Jobs");
                    RaisePropertyChanged("Org");
                    break;
                case ChangeType.DayAdvanced:
                    RaisePropertyChanged("Jobs");
                    RaisePropertyChanged("Org");
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
