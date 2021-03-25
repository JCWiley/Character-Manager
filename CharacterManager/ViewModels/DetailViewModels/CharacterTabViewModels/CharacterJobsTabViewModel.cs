using CharacterManager.Events;
using CharacterManager.Events.EventContainers;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.Providers;
using CharacterManager.Model.RedundantTree;
using CharacterManager.ViewModels.Helpers;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.ViewModels.DetailViewModels.CharacterTabViewModels
{
    public class CharacterJobsTabViewModel : BindableBase
    {
        public CharacterJobsTabViewModel(IEntityProvider entityProvider, IEventAggregator eventAggregator, IRegionManager regionManager, IJobDirectoryProvider jobDirectoryProvider, IDialogServiceHelper dialogServiceHelper)
        {
            EA = eventAggregator;
            RM = regionManager;
            EP = entityProvider;
            JDP = jobDirectoryProvider;
            DSH = dialogServiceHelper;

            EA.GetEvent<UIUpdateRequestEvent>().Subscribe(UIUpdateRequestExecute);
        }

        #region Variables
        private IEventAggregator EA;
        private IRegionManager RM;
        private IEntityProvider EP;
        private IDialogServiceHelper DSH;
        private IJobDirectoryProvider JDP;
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
                RaisePropertyChanged("Char");
                RaisePropertyChanged("Jobs");
            }
        }
        public List<IJob> Jobs
        {
            get
            {
                return JDP.GetEntitiesJobs(EP.CurrentTargetAsCharacter);
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
                if(SelectedJob is IJob)
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

        private DelegateCommand<object> _commandselectedjobchanged;
        public DelegateCommand<object> CommandSelectedJobChanged => _commandselectedjobchanged ??= new DelegateCommand<object>(CommandSelectedJobChangedExecute);
        #endregion

        #region Command Handlers
        private void CommandNewBlankJobExecute()
        {
            SelectedJob = JDP.AddBlankJobToEntity(EP.CurrentTargetAsCharacter);
            RaisePropertyChanged("IsJobEnabled");
            RaisePropertyChanged("Jobs");
        }

        private void CommandAddCustomEventExecute()
        {
            //DSH.ShowNewEventPopup(CustomEventCreated, SelectedJob,EP.CurrentTargetAsCharacter);
            EA.GetEvent<RequestJobEventEvent>().Publish(new JobEventRequestContainer(SelectedJob, 0));
            RaisePropertyChanged("Jobs");
        }
        private void CommandSelectedJobChangedExecute(object J)
        {
            SelectedJob = (IJob)J;
            RaisePropertyChanged("IsJobEnabled");
        }
        #endregion

        #region Event Handlers
        private void UIUpdateRequestExecute(ChangeType type)
        {
            switch (type)
            {
                case ChangeType.SelectedCharacterChanged:
                    //SelectedJob = null;

                    RaisePropertyChanged("IsEntityEnabled");
                    RaisePropertyChanged("IsJobEnabled");

                    RaisePropertyChanged("Char");
                    RaisePropertyChanged("Jobs");
                    break;
                case ChangeType.SelectedOrganizationChanged:
                    break;
                case ChangeType.JobEventListChanged:
                    break;
                case ChangeType.JobListChanged:
                    RaisePropertyChanged("Jobs");
                    RaisePropertyChanged("Char");
                    break;
                case ChangeType.DayAdvanced:
                    RaisePropertyChanged("Jobs");
                    RaisePropertyChanged("Char");
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
