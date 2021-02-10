using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.RedundantTree;
using CharacterManager.Model.Services;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace CharacterManager.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        public ShellViewModel (IEventAggregator eventAggregator, IRegionManager regionManager, ISettingsService settingsService)
        {
            RM = regionManager;
            EA = eventAggregator;
            SS = settingsService;

            EA.GetEvent<SelectedEntityChangedEvent>().Subscribe(SelectedEntityChangedExecute);
            RM.RequestNavigate("DETAIL_REGION", "OrganizationDetailView");
            EA.GetEvent<DataLoadRequestEvent>().Publish(LoadRequestTypes.LastFile);
        }

        #region Variables
        private IEventAggregator EA;
        private readonly IRegionManager RM;
        private ISettingsService SS;
        #endregion

        #region Binding Targets
        public int OverviewColumnWidth
        {
            get
            {
                return SS.OverviewColumnWidth;
            }
            set
            {
                SS.OverviewColumnWidth = value;
                RaisePropertyChanged("OverviewColumnWidth");
                RaisePropertyChanged("DetailColumnWidth");
            }
        }
        public int DetailColumnWidth
        {
            get
            {
                return SS.DetailColumnWidth;
            }
            set
            {
                SS.DetailColumnWidth = value;
                RaisePropertyChanged("OverviewColumnWidth");
                RaisePropertyChanged("DetailColumnWidth");
            }
        }

        public string Filename
        {
            get
            {
                return SS.Filename;
            }
        }
        #endregion

        #region Event Handlers
        void SelectedEntityChangedExecute(IRTreeMember<IEntity> Selected_Item)
        {
            if (Selected_Item.Item is Character)
            {
                RM.RequestNavigate("DETAIL_REGION", "CharacterDetailView");
            }
            else if (Selected_Item.Item is Organization)
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
