using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.Providers;
using CharacterManager.Model.RedundantTree;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.ViewModels.DetailViewModels.CharacterTabViewModels
{
    public class CharacterJobsTabViewModel : BindableBase
    {
        public CharacterJobsTabViewModel(IEntityProvider entityProvider, IEventAggregator eventAggregator, IRegionManager regionManager, IJobDirectoryProvider jobDirectoryProvider)
        {
            EA = eventAggregator;
            RM = regionManager;
            EP = entityProvider;
            JDP = jobDirectoryProvider;

            EA.GetEvent<SelectedEntityChangedPostEvent>().Subscribe(SelectedEntityChangedPostEventExecute);
        }

        #region Variables
        private IEventAggregator EA;
        private IRegionManager RM;
        private IEntityProvider EP;
        private IJobDirectoryProvider JDP;


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
                return JDP.GetEntitiesJobs(EP.CurrentTargetAsCharacter.Item);
            }
        }
        #endregion
        #region Commands
        private DelegateCommand _commandnewblankjob;
        public DelegateCommand CommandNewBlankJob => _commandnewblankjob ??= new DelegateCommand(CommandNewBlankJobExecute);

        private DelegateCommand _commandaddcustomevent;

        public DelegateCommand CommandAddCustomEvent => _commandaddcustomevent ??= new DelegateCommand(CommandAddCustomEventExecute);

        #endregion

        #region Command Handlers
        private void CommandNewBlankJobExecute()
        {
            JDP.AddBlankJobToEntity(EP.CurrentTargetAsCharacter.Item);
            RaisePropertyChanged("Jobs");
        }

        private void CommandAddCustomEventExecute()
        {

        }

        #endregion

        #region Event Handlers
        private void SelectedEntityChangedPostEventExecute(EntityTypes type)
        {
            if(type == EntityTypes.Character)
            {
                RaisePropertyChanged("Jobs");
            }
        }
        #endregion
    }
}
