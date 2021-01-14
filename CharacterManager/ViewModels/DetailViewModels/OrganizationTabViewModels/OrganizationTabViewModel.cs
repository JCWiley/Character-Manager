using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Providers;
using CharacterManager.Model.RedundantTree;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;

namespace CharacterManager.ViewModels.DetailViewModels.OrganizationTabViewModels
{
    public class OrganizationTabViewModel : BindableBase
    {
        public OrganizationTabViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IDerivedDataProvider derivedDataProvider, IEntityProvider entityProvider)
        {
            EA = eventAggregator;
            RM = regionManager;
            EP = entityProvider;
            DDP = derivedDataProvider;
        }

        #region Variables
        private IEventAggregator EA;
        private IRegionManager RM;

        private IDerivedDataProvider ddp;
        private IEntityProvider EP;
        public IDerivedDataProvider DDP
        {
            get { return ddp; }
            set { SetProperty(ref ddp, value); }
        }
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
            }
        }
        public List<IRTreeMember<IEntity>> ImmidiateChildren
        {
            get
            {
                return EP.GetImmidiateChildren(EP.CurrentTargetAsOrganization);
            }
        }
        #endregion
    }
}
