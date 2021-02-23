using CharacterManager.Events;
using CharacterManager.Model.Entities;
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
    public class OrganizationInventoryTabViewModel : BindableBase
    {
        public OrganizationInventoryTabViewModel(IEntityProvider entityProvider, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            EA = eventAggregator;
            RM = regionManager;
            EP = entityProvider;

            EA.GetEvent<UIUpdateRequestEvent>().Subscribe(UIUpdateRequestExecute);
        }

        #region Variables
        private IEventAggregator EA;
        private IRegionManager RM;
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
                    RaisePropertyChanged("IsEntityEnabled");

                    RaisePropertyChanged("Org");
                    break;
                case ChangeType.JobEventListChanged:
                    break;
                case ChangeType.JobListChanged:
                    break;
                case ChangeType.DayAdvanced:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
