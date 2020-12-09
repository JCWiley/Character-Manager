using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Providers;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;

namespace CharacterManager.ViewModels.DetailViewModels.CharacterTabViewModels
{
    public class CharacterJobHistoryTabViewModel : BindableBase
    {
        public CharacterJobHistoryTabViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IJobDirectoryProvider jobDirectoryProvider)
        {
            EA = eventAggregator;
            RM = regionManager;
            JDP = jobDirectoryProvider;

            EA.GetEvent<SelectedEntityChangedEvent>().Subscribe(SelectedEntityChangedExecute);
        }

        #region Variables
        private IEventAggregator EA;
        private IRegionManager RM;
        private IJobDirectoryProvider JDP;

        private Character target;
        public Character Target
        {
            get { return target; }
            set 
            {
                SetProperty(ref target, value);
                RaisePropertyChanged("Events_Summary");
            }
        }
        #endregion

        #region Binding Targets
        public List<IEvent> Events_Summary
        {
            get
            {
                return JDP.GetEventSummaryForEntity(Target);
            }
        }

        #endregion

        #region EventHandlers
        private void SelectedEntityChangedExecute(IEntity newTarget)
        {
            if (newTarget is Character C)
            {
                Target = C;
            }
            else if (newTarget is Organization)
            {

            }
            else
            {
                throw new Exception("CharacterTabViewModel newTarget is not Character or Organization");
            }
        }
        #endregion
    }
}
