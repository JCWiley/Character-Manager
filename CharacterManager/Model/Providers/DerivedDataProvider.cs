using CharacterManager.Model.Entities;
using CharacterManager.Model.RedundantTree;
using CharacterManager.Model.Services;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace CharacterManager.Model.Providers
{
    public class DerivedDataProvider : BindableBase,IDerivedDataProvider
    {
        IDataService DS;

        public DerivedDataProvider(IDataService dataService)
        {
            DS = dataService;
        }

        #region Races
        [JsonIgnore]
        [IgnoreDataMember]
        private ObservableCollection<String> races = null;

        [JsonIgnore]
        [IgnoreDataMember]
        public ObservableCollection<string> Races
        {
            get
            {
                if (races is null)
                {
                    UpdateRacesList();
                }
                return races;
            }
        }
        public void UpdateRacesList()
        {
            if(races is null)
            {
                races = new ObservableCollection<string>();
            }

            foreach (IEntity entity in DS.EntityTree.Get_All_Items())
            {
                if (!string.IsNullOrWhiteSpace(entity.Race))
                {
                    if (!races.Contains(entity.Race))
                    {
                        races.Add(entity.Race);
                    }
                }
            }
            RaisePropertyChanged("Races");
        }

        #endregion
        #region Locations
        [JsonIgnore]
        [IgnoreDataMember]
        private ObservableCollection<String> locations = null;

        [JsonIgnore]
        [IgnoreDataMember]
        public ObservableCollection<string> Locations
        {
            get
            {
                if (locations is null)
                {
                    UpdateLocationsList();
                }
                return locations;
            }
        }

        public void UpdateLocationsList()
        {
            if(locations is null)
            {
                locations = new ObservableCollection<string>();
            }

            foreach (IEntity entity in DS.EntityTree.Get_All_Items())
            {
                if(!string.IsNullOrWhiteSpace(entity.Location))
                {
                    if (!locations.Contains(entity.Location))
                    {
                        locations.Add(entity.Location);
                    }
                }
                if(entity is Character c)
                {
                    if (!string.IsNullOrWhiteSpace(c.BirthPlace))
                    {
                        if (!locations.Contains(c.BirthPlace))
                        {
                            locations.Add(c.BirthPlace);
                        }
                    }
                }
            }
            RaisePropertyChanged("Locations");
        }
        #endregion
    }
}
