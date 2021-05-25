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

        [JsonIgnore]
        [IgnoreDataMember]
        private ObservableCollection<String> locations = new ObservableCollection<string>();

        [JsonIgnore]
        [IgnoreDataMember]
        public ObservableCollection<string> Locations
        {
            get
            {
                if(locations.Count==0)
                {
                    UpdateLocationsList();
                }
                return locations;
            }
        }

        public void UpdateLocationsList()
        {
            foreach (IEntity entity in DS.EntityTree.Get_All_Items())
            {
                if(!string.IsNullOrWhiteSpace(entity.Location))
                {
                    if (!locations.Contains(entity.Location))
                    {
                        locations.Add(entity.Location);
                    }
                }
            }
            RaisePropertyChanged("Locations");
        }
    }
}
