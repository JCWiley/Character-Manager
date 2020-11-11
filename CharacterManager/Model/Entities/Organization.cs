using Character_Manager.Model.Other;
using CharacterManager.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace CharacterManager.Model.Entities
{
    public class Organization : IEntity
    {
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

        [DataMember(Name = "leader")]
        private IEntity leader;
        public IEntity Leader
        {
            get
            {
                return this.leader;
            }
            set
            {
                if (this.leader != value)
                {
                    this.leader = value;
                }
            }
        }

        [DataMember(Name = "goals")]
        private string goals;
        public string Goals
        {
            get
            {
                return this.goals;
            }
            set
            {
                if (this.goals != value)
                {
                    this.goals = value;
                }
            }
        }

        [DataMember(Name = "selected_size")]
        private int selected_size;
        public int Selected_Size
        {
            get
            {
                return this.selected_size;
            }
            set
            {
                if (this.selected_size != value)
                {
                    this.selected_size = value;
                }
            }
        }
    }
}
