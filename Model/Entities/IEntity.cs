using Character_Manager.GeneralInterfaces;
using Character_Manager.Model.Collection_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager.Model.Entities
{
    public interface IEntity : INotifyPropertyChanged, IVisible
    {
        #region Data_Members
        public Item_Collection Inventory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Quirks { get; set; }
        public string Location { get; set; }
        public string Race { get; set; }
        #endregion
    }
}
