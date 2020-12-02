﻿using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Interfaces;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.ViewModels.DetailViewModels.CharacterTabViewModels
{
    public class CharacterTabViewModel : BindableBase
    {
        public CharacterTabViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IDerivedDataProvider derivedDataProvider)
        {
            EA = eventAggregator;
            RM = regionManager;
            DDP = derivedDataProvider;

            EA.GetEvent<SelectedEntityChangedEvent>().Subscribe(SelectedEntityChangedExecute);
        }

        #region Variables
        private IEventAggregator EA;
        private IRegionManager RM;

        private IDerivedDataProvider ddp;
        public IDerivedDataProvider DDP
        {
            get { return ddp; }
            set { SetProperty(ref ddp, value); }
        }


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
