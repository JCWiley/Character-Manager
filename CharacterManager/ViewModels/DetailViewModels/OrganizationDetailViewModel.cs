using System;
using Prism.Mvvm;
using Prism.Events;
using Prism.Regions;
using CharacterManager.Model.Entities;
using CharacterManager.Events;
using CharacterManager.Model.RedundantTree;

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
        #endregion
    }
}
