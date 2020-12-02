using CharacterManager.Model.Other;
using CharacterManager.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace CharacterManager.Model.Entities
{
    public class Character : IEntity
    {
        public Character()
        {
            Inventory = new ObservableCollection<Item>();
            Name = "New Character";
            Description = "3";
            Quirks = "4";
            Location = "";
            Race = "";
            Alias = "";
            Occupation = "";
            BirthPlace = "";
        }

        [DataMember(Name = "inventory")]
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
                }
            }
        }

        [DataMember(Name = "name")]
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
                }
            }
        }

        [DataMember(Name = "description")]
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
                }
            }
        }

        [DataMember(Name = "quirks")]
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
                }
            }
        }

        [DataMember(Name = "location")]
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
                }
            }
        }

        [DataMember(Name = "race")]
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
                }
            }
        }

        [DataMember(Name = "alias")]
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
                }
            }
        }

        [DataMember(Name = "occupation")]
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
                }
            }
        }

        [DataMember(Name = "birthplace")]
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
                }
            }
        }


    }
}
