using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Providers;
using CharacterManager.Model.RedundantTree;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace CharacterManager.ViewModels.DetailViewModels.CharacterTabViewModels
{
    public class CharacterTabViewModel : BindableBase
    {
        public CharacterTabViewModel(IEntityProvider entityProvider, IEventAggregator eventAggregator, IRegionManager regionManager, IDerivedDataProvider derivedDataProvider)
        {
            EA = eventAggregator;
            RM = regionManager;
            EP = entityProvider;
            DDP = derivedDataProvider;

            EA.GetEvent<SelectedEntityChangedPostEvent>().Subscribe(SelectedEntityChangedPostEventExecute);
        }

        #region Variables
        private IEventAggregator EA;
        private IRegionManager RM;
        private IEntityProvider EP;

        private IDerivedDataProvider ddp;
        public IDerivedDataProvider DDP
        {
            get { return ddp; }
            set { SetProperty(ref ddp, value); }
        }
        public Character Char
        {
            get
            {
                if (EP.CurrentTargetAsCharacter != null)
                {
                    return (Character)EP.CurrentTargetAsCharacter.Item;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                EP.CurrentTargetAsCharacter.Item = value;
                RaisePropertyChanged("Char");
            }
        }
        #endregion

        #region Event Handlers
        private void SelectedEntityChangedPostEventExecute(EntityTypes type)
        {
            if (type == EntityTypes.Character)
            {
                RaisePropertyChanged("Char");
            }
        }
        #endregion
    }
}
