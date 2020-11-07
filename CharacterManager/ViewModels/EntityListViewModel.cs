using CharacterManager.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.ViewModels
{
    public class EntityListViewModel : BindableBase
    {
        private IEventAggregator EA;

        public EntityListViewModel(IEventAggregator eventAggregator)
        {
            EA = eventAggregator;
        }

        private DelegateCommand selecteditemchangedcommand;
        public DelegateCommand SelectedItemChangedCommand => selecteditemchangedcommand ??= new DelegateCommand(SelectedItemChangedExecute);

        private void SelectedItemChangedExecute()
        {
            //EA.GetEvent<SelectedEntityChangedEvent>().Publish(//selected entity);
        }
    }
}
