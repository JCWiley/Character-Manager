using CharacterManager.Model.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CharacterManager.Model.Entities
{
    public interface IEntity 
    {
        #region Data_Members
        public ObservableCollection<Item> Inventory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Quirks { get; set; }
        public string Location { get; set; }
        public string Race { get; set; }
        #endregion
    }
}
