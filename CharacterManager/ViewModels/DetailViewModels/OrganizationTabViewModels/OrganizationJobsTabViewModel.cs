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

            EA.GetEvent<SelectedEntityChangedPostEvent>().Subscribe(SelectedEntityChangedPostEventExecute);
        }

        #region Variables
        private IEventAggregator EA;
        private IRegionManager RM;
        private IJobDirectoryProvider JDP;
        private IEntityProvider EP;
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
            set
            {
                EP.CurrentTargetAsOrganization.Item = value;
                RaisePropertyChanged("Org");
                RaisePropertyChanged("Jobs");
            }
        }
        public List<IRTreeMember<IEntity>> Entities
        {
            get
            {
                return EP.CurrentTargetAsOrganization.Child_Items;

                //List<IRTreeMember<IEntity>> entities = new List<IEntity>();
                //foreach (var item in EP.CurrentTargetAsOrganization.Child_Items)
                //{
                //    entities.Add(item);
                //}
                //return entities;
            }
        }
        public List<IJob> Jobs
        {
            get
            {
                return JDP.GetEntitiesJobs(EP.CurrentTargetAsOrganization.Item);
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
            SelectedJob = JDP.AddBlankJobToEntity(EP.CurrentTargetAsOrganization.Item);
            RaisePropertyChanged("Jobs");
        }

        private void CommandAddCustomEventExecute()
        {

        }

        private void CommandAddSubtaskExecute()
        {
            JDP.AddBlankJobToJob(SelectedJob);
            RaisePropertyChanged("ChildJobs");
        }

        private void CommandSelectedJobChangedExecute(object J)
        {
            SelectedJob = (IJob)J;
        }

        #endregion

        #region Event Handlers
        private void SelectedEntityChangedPostEventExecute(EntityTypes type)
        {
            if (type == EntityTypes.Organization)
            {
                RaisePropertyChanged("Jobs");
            }
        }
        #endregion
    }
}
