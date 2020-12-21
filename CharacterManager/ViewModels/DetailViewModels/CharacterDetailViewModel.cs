using System;

using Prism.Mvvm;
using Prism.Events;
using Prism.Regions;
using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.RedundantTree;

namespace CharacterManager.ViewModels.DetailViewModels
{
    public class CharacterDetailViewModel : BindableBase
    {
        public CharacterDetailViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
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
