using CharacterManager.Model.Entities;
using CharacterManager.Model.RedundantTree;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CharacterManager.Model.Providers
{
    public interface IDerivedDataProvider
    {
        public ObservableCollection<String> Locations { get;}
        public void UpdateLocationsList();
    }
}
