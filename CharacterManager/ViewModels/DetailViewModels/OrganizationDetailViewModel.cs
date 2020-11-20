using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;
using Prism.Events;

namespace CharacterManager.ViewModels.DetailViewModels
{
    public class OrganizationDetailViewModel : BindableBase
    {
        public OrganizationDetailViewModel(IEventAggregator eventAggregator)
        {
            EA = eventAggregator;
        }

        private IEventAggregator EA;
    }
}
