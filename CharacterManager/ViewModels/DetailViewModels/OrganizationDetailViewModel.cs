using System;
using Prism.Mvvm;
using Prism.Events;
using Prism.Regions;
using CharacterManager.Model.Entities;

namespace CharacterManager.ViewModels.DetailViewModels
{
    public class OrganizationDetailViewModel : BindableBase
    {
        public OrganizationDetailViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            EA = eventAggregator;
            RM = regionManager;
        }

        #region Variables
        private IEventAggregator EA;
        private IRegionManager RM;

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
                throw new Exception("Organization Detail newTarget is not Character or Organization");
            }
        }
        #endregion
    }
}
