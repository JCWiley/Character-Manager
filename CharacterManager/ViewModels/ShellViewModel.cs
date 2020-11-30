using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Interfaces;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.ViewModels
{
    public class ShellViewModel
    {
        public ShellViewModel (IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            RM = regionManager;
            EA = eventAggregator;
            EA.GetEvent<SelectedEntityChangedEvent>().Subscribe(SelectedEntityChangedExecute);
        }

        #region Variables
        private IEventAggregator EA;
        private readonly IRegionManager RM;
        #endregion

        #region Event Handlers
        void SelectedEntityChangedExecute(IEntity Selected_Item)
        {
            if (Selected_Item is Character)
            {
                RM.RequestNavigate("DETAIL_REGION", "CharacterDetailView");
            }
            else if (Selected_Item is Organization)
            {
                RM.RequestNavigate("DETAIL_REGION", "OrganizationDetailView");
            }
            else
            {
                throw new Exception("Selected_Item is not Character or Organization");
            }
        }

        #endregion
    }
}
