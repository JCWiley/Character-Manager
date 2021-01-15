using CharacterManager.Model.Items;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Prism.Mvvm;
using System.Collections.Generic;

namespace CharacterManager.Model.Entities
{

    public class Character : BindableBase,IEntity
    {
        public Character()
        {
            Inventory = new ObservableCollection<Item>();
            Name = "New Character";
            Description = "{}";
            Quirks = "{}";
            Location = "";
            Race = "";
            Alias = "";
            Occupation = "";
            BirthPlace = "";
        }

        private ObservableCollection<Item> inventory;
        public ObservableCollection<Item> Inventory
        {
            get
            {
                return this.inventory;
            }
            set
            {
                if (this.inventory != value)
                {
                    this.inventory = value;
                    RaisePropertyChanged("Inventory");
                }
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    RaisePropertyChanged("Description");
                }
            }
        }

        private string quirks;
        public string Quirks
        {
            get
            {
                return this.quirks;
            }
            set
            {
                if (this.quirks != value)
                {
                    this.quirks = value;
                    RaisePropertyChanged("Quirks");
                }
            }
        }

        private string location;
        public string Location
        {
            get
            {
                return this.location;
            }
            set
            {
                if (this.location != value)
                {
                    this.location = value;
                    RaisePropertyChanged("Location");
                    RaisePropertyChanged("Locations");
                }
            }
        }

        private string race;
        public string Race
        {
            get
            {
                return this.race;
            }
            set
            {
                if (this.race != value)
                {
                    this.race = value;
                    RaisePropertyChanged("Race");
                }
            }
        }

        private string alias;
        public string Alias
        {
            get
            {
                return this.alias;
            }
            set
            {
                if (this.alias != value)
                {
                    this.alias = value;
                    RaisePropertyChanged("Alias");
                }
            }
        }

        private string occupation;
        public string Occupation
        {
            get
            {
                return this.occupation;
            }
            set
            {
                if (this.occupation != value)
                {
                    this.occupation = value;
                    RaisePropertyChanged("Occupation");
                }
            }
        }

        private string birthplace;
        public string BirthPlace
        {
            get
            {
                return this.birthplace;
            }
            set
            {
                if (this.birthplace != value)
                {
                    this.birthplace = value;
                    RaisePropertyChanged("BirthPlace");
                }
            }
        }
    }
}
