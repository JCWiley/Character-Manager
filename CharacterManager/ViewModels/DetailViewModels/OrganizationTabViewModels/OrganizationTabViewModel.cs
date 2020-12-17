using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Providers;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace CharacterManager.ViewModels.DetailViewModels.OrganizationTabViewModels
{
    public class OrganizationTabViewModel : BindableBase
    {
        public OrganizationTabViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IDerivedDataProvider derivedDataProvider)
        {
            EA = eventAggregator;
            RM = regionManager;
            DDP = derivedDataProvider;

            EA.GetEvent<SelectedEntityChangedEvent>().Subscribe(SelectedEntityChangedExecute);
        }

        #region Variables
        private IEventAggregator EA;
        private IRegionManager RM;

        private IDerivedDataProvider ddp;
        public IDerivedDataProvider DDP
        {
            get { return ddp; }
            set { SetProperty(ref ddp, value); }
        }


        private Organization target;
        public Organization Target
        {
            get { return target; }
            set { SetProperty(ref target, value); }
        }
        #endregion

        #region EventHandlers
        private void SelectedEntityChangedExecute(IEntity newTarget)
        {
            if (newTarget is Character C)
            {
                
            }
            else if (newTarget is Organization O)
            {
                Target = O;
            }
            else
            {
                throw new Exception("OrganizationTabViewModel newTarget is not Character or Organization");
            }
        }
        #endregion
    }
}
