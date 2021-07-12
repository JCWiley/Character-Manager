using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Providers;
using Prism.Events;
using Prism.Mvvm;

namespace CharacterManager.ViewModels.DetailViewModels.OrganizationTabViewModels
{
    public class OrganizationInventoryTabViewModel : BindableBase
    {
        public OrganizationInventoryTabViewModel(IEntityProvider entityProvider, IEventAggregator eventAggregator)
        {
            EA = eventAggregator;
            EP = entityProvider;

            EA.GetEvent<UIUpdateRequestEvent>().Subscribe( UIUpdateRequestExecute );
        }

        #region Variables
        private readonly IEventAggregator EA;
        private readonly IEntityProvider EP;
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

                    RaisePropertyChanged( nameof( Org ) );
                    break;
                case ChangeType.JobEventListChanged:
                    break;
                case ChangeType.JobListChanged:
                    break;
                case ChangeType.DayAdvanced:
                    break;
                case ChangeType.EntityListChanged:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
