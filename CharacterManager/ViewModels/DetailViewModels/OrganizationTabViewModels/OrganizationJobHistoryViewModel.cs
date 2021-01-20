using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Providers;
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
    public class OrganizationJobHistoryViewModel : BindableBase
    {
        public OrganizationJobHistoryViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IJobDirectoryProvider jobDirectoryProvider, IEntityProvider entityProvider)
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
        #endregion

        #region Binding targets
        List<IEvent> JobEventSummary
        {
            get
            {

            }
        }
        #endregion

        #region Event Handlers
        private void SelectedEntityChangedPostEventExecute(EntityTypes type)
        {
            if (type == EntityTypes.Organization)
            {
                RaisePropertyChanged("JobEventSummary");
            }
        }
        #endregion
    }
}
