
using Prism.Events;
using Prism.Mvvm;
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


        #region Variables
        private readonly IEventAggregator EA;
        private readonly IRegionManager RM;
        #endregion


    }

}
