using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.Providers;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.ViewModels.DetailViewModels.OrganizationTabViewModels
{
    public class OrganizationJobsTabViewModel : BindableBase
    {
        public OrganizationJobsTabViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IJobDirectoryProvider jobDirectoryProvider)
        {
            EA = eventAggregator;
            RM = regionManager;
            JDP = jobDirectoryProvider;

            EA.GetEvent<SelectedEntityChangedEvent>().Subscribe(SelectedEntityChangedExecute);
        }

        #region Variables
        private IEventAggregator EA;
        private IRegionManager RM;
        private IJobDirectoryProvider JDP;

        private Organization target;
        public Organization Target
        {
            get { return target; }
            set
            {
                SetProperty(ref target, value);
                RaisePropertyChanged("Jobs");
            }
        }
        #endregion

        #region Binding Targets
        public List<IJob> Jobs
        {
            get
            {
                return JDP.GetEntitiesJobs(Target);
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

        #endregion

        #region Command Handlers
        private void CommandNewBlankJobExecute()
        {
            JDP.AddBlankJobToEntity(Target);
            RaisePropertyChanged("Jobs");
        }

        private void CommandAddCustomEventExecute()
        {

        }

        private void CommandAddSubtaskExecute()
        {

        }

        #endregion

        #region EventHandlers
        private void SelectedEntityChangedExecute(IEntity newTarget)
        {
            if (newTarget is Character)
            {

            }
            else if (newTarget is Organization O)
            {
                Target = O;
                RaisePropertyChanged("Jobs");
            }
            else
            {
                throw new Exception("CharacterTabViewModel newTarget is not Character");
            }
        }
        #endregion
    }
}
