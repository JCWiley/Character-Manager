using Character_Manager.Model.Other;
using CharacterManager.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CharacterManager.Model.Entities
{
    public class Character : IEntity
    {
        public ObservableCollection<Item> Inventory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Quirks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Location { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Race { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
