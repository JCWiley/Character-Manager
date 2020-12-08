using CharacterManager.Model.Items;
using System;
using System.Collections.ObjectModel;

namespace CharacterManager.Model.Entities
{
    public interface IEntity 
    {
        #region Data_Members
        public ObservableCollection<IItem> Inventory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Quirks { get; set; }
        public string Location { get; set; }
        public string Race { get; set; }
        public Guid Job_ID { get; }
        #endregion
    }
}
