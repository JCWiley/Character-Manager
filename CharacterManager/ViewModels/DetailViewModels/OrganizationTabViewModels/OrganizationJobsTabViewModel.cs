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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.ViewModels.DetailViewModels.OrganizationTabViewModels
{
    public class OrganizationJobsTabViewModel : BindableBase
    {
        public OrganizationJobsTabViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IJobDirectoryProvider jobDirectoryProvider, IEntityProvider entityProvider)
        {
            EA = eventAggregator;
            RM = regionManager;
            JDP = jobDirectoryProvider;
            EP = entityProvider;
        }

        #region Variables
        private IEventAggregator EA;
        private IRegionManager RM;
        private IJobDirectoryProvider JDP;
        private IEntityProvider EP;


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
            set
            {
                EP.CurrentTargetAsOrganization.Item = value;
                RaisePropertyChanged("Org");
                RaisePropertyChanged("Jobs");
            }
        }
        public List<IJob> Jobs
        {
            get
            {
                return JDP.GetEntitiesJobs(EP.CurrentTargetAsOrganization.Item);
            }
        }
        public List<IRTreeMember<IEntity>> TargetChildren
        {
            get
            {
                return EP.GetImmidiateChildren(EP.CurrentTargetAsOrganization);
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
            JDP.AddBlankJobToEntity(EP.CurrentTargetAsOrganization.Item);
            RaisePropertyChanged("Jobs");
        }

        private void CommandAddCustomEventExecute()
        {

        }

        private void CommandAddSubtaskExecute()
        {

        }

        #endregion
    }
}
