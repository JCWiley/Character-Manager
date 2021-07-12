using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Providers;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;

namespace CharacterManager.ViewModels.DetailViewModels.OrganizationTabViewModels
{
    public class OrganizationJobHistoryTabViewModel : BindableBase
    {
        public OrganizationJobHistoryTabViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IJobDirectoryProvider jobDirectoryProvider, IEntityProvider entityProvider, IJobEventProvider jobEventProvider)
        {
            EA = eventAggregator;
            RM = regionManager;
            JDP = jobDirectoryProvider;
            EP = entityProvider;
            JEP = jobEventProvider;

            EA.GetEvent<UIUpdateRequestEvent>().Subscribe( UIUpdateRequestExecute );
        }

        #region Variables
        private readonly IEventAggregator EA;
        private readonly IRegionManager RM;
        private readonly IJobDirectoryProvider JDP;
        private readonly IEntityProvider EP;
        private readonly IJobEventProvider JEP;
        #endregion

        #region Binding targets
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
        public List<IEvent> JobEventSummary
        {
            get
            {
                return JEP.GetEventsForEntity( EP.CurrentTargetAsOrganization );
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
        #endregion

        #region Event Handlers
        private void UIUpdateRequestExecute(ChangeType type)
        {
            switch (type)
            {
                case ChangeType.SelectedCharacterChanged:
                    break;
                case ChangeType.SelectedOrganizationChanged:
                    RaisePropertyChanged( nameof( IsEntityEnabled ) );

                    RaisePropertyChanged( nameof( JobEventSummary ) );
                    break;
                case ChangeType.JobEventListChanged:
                    RaisePropertyChanged( nameof( JobEventSummary ) );
                    break;
                case ChangeType.JobListChanged:
                    break;
                case ChangeType.DayAdvanced:
                    RaisePropertyChanged( nameof( JobEventSummary ) );
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
