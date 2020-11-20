using System;
using System.Collections.Generic;
using System.Text;

using Prism.Mvvm;
using Prism.Events;
using Prism.Regions;

namespace CharacterManager.ViewModels.DetailViewModels
{
    public class CharacterDetailViewModel : BindableBase
    {
        public CharacterDetailViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            EA = eventAggregator;
            RM = regionManager;
        }

        private IEventAggregator EA;
        private IRegionManager RM;
    }
}
