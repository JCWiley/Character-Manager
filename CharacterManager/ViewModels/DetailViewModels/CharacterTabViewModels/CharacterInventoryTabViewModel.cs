using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Items;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;

namespace CharacterManager.ViewModels.DetailViewModels.CharacterTabViewModels
{
    public class CharacterInventoryTabViewModel : BindableBase
    {
        public CharacterInventoryTabViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            EA = eventAggregator;
            RM = regionManager;

            EA.GetEvent<SelectedEntityChangedEvent>().Subscribe(SelectedEntityChangedExecute);
        }

        #region Variables
        private IEventAggregator EA;
        private IRegionManager RM;

        private Character target;
        public Character Target
        {
            get { return target; }
            set { SetProperty(ref target, value); }
        }
        #endregion

        public ObservableCollection<IItem> Inventory
        {
            get
            {
                if (target is null)
                {
                    return new ObservableCollection<IItem>();
                }
                else
                {
                    return target.Inventory;
                }
            }
            set
            {
                if (target.Inventory != value)
                {
                    target.Inventory = value;
                    RaisePropertyChanged("Inventory");
                }
            }
        }

        #region EventHandlers
        private void SelectedEntityChangedExecute(IEntity newTarget)
        {
            if (newTarget is Character C)
            {
                Target = C;
            }
            else if (newTarget is Organization O)
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
