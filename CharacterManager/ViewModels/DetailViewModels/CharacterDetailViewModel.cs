using System;

using Prism.Mvvm;
using Prism.Events;
using Prism.Regions;
using CharacterManager.Events;
using CharacterManager.Model.Entities;

namespace CharacterManager.ViewModels.DetailViewModels
{
    public class CharacterDetailViewModel : BindableBase
    {
        public CharacterDetailViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
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

        #region EventHandlers
        private void SelectedEntityChangedExecute(IEntity newTarget)
        {
            if(newTarget is Character C)
            {
                Target = C;
            }
            else if(newTarget is Organization O)
            {

            }
            else
            {
                throw new Exception("Character Detail newTarget is not Character or Organization");
            }
        }
        #endregion


    }

}
