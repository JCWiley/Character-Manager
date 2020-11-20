using System;
using System.Collections.Generic;
using System.Text;

using Prism.Mvvm;
using Prism.Events;



namespace CharacterManager.ViewModels.DetailViewModels
{
    public class CharacterDetailViewModel : BindableBase
    {
        public CharacterDetailViewModel(IEventAggregator eventAggregator)
        {
            EA = eventAggregator;
        }

        private IEventAggregator EA;
    }
}
