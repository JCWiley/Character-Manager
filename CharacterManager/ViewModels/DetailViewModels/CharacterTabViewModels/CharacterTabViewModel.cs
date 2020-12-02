using CharacterManager.Events;
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
        private IDerivedDataProvider DDP;

        private Character target;
        public Character Target
        {
            get { return target; }
            set { SetProperty(ref target, value); }
        }
        #endregion

        #region BindingTargets
        public string Occupation
        {
            get 
            {
                if(target is null)
                {
                    return "";
                }
                else
                {
                    return target.Occupation;
                }
            }
            set 
            {
                if(target.Occupation != value)
                {
                    target.Occupation = value;
                    RaisePropertyChanged(Occupation);
                }
            }
        }
        public string Race
        {
            get
            {
                if (target is null)
                {
                    return "";
                }
                else
                {
                    return target.Race;
                }
            }
            set
            {
                if (target.Race != value)
                {
                    target.Race = value;
                    RaisePropertyChanged(Race);
                }
            }
        }
        public string Name
        {
            get
            {
                if (target is null)
                {
                    return "";
                }
                else
                {
                    return target.Name;
                }
            }
            set
            {
                if (target.Name != value)
                {
                    target.Name = value;
                    RaisePropertyChanged(Name);
                }
            }
        }
        public string Alias
        {
            get
            {
                if (target is null)
                {
                    return "";
                }
                else
                {
                    return target.Alias;
                }
            }
            set
            {
                if (target.Alias != value)
                {
                    target.Alias = value;
                    RaisePropertyChanged(Alias);
                }
            }
        }
        public string BirthPlace
        {
            get
            {
                if (target is null)
                {
                    return "";
                }
                else
                {
                    return target.BirthPlace;
                }
            }
            set
            {
                if (target.BirthPlace != value)
                {
                    target.BirthPlace = value;
                    RaisePropertyChanged(BirthPlace);
                }
            }
        }
        public string Location
        {
            get
            {
                if (target is null)
                {
                    return "";
                }
                else
                {
                    return target.Location;
                }
            }
            set
            {
                if (target.Location != value)
                {
                    target.Location = value;
                    RaisePropertyChanged(Location);
                }
            }
        }
        public string Description
        {
            get
            {
                if (target is null)
                {
                    return "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>";
                }
                else
                {
                    return target.Description;
                }
            }
            set
            {
                if (target.Description != value)
                {
                    target.Description = value;
                    RaisePropertyChanged(Description);
                }
            }
        }
        public string Quirks
        {
            get
            {
                if (target is null)
                {
                    return "2";
                }
                else
                {
                    return target.Quirks;
                }
            }
            set
            {
                if (target.Quirks != value)
                {
                    target.Quirks = value;
                    RaisePropertyChanged(Quirks);
                }
            }
        }

        public List<String> Locations
        {
            get
            {
                return DDP.Locations;
            }
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
