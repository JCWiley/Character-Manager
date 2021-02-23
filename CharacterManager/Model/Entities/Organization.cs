﻿using CharacterManager.Model.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;
using Prism.Mvvm;
using CharacterManager.Model.RedundantTree;

namespace CharacterManager.Model.Entities
{
    public class Organization : BindableBase, IEntity
    {
        public Organization()
        {
            Inventory = new ObservableCollection<Item>();
            Name = "New Organization";
            Description = "{}";
            Quirks = "{}";
            Location = "";
            Race = "";
            //Leader = "";
            Goals = "";
            Selected_Size = 0;
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

        private IRTreeMember<IEntity> leader;
        public IRTreeMember<IEntity> Leader
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
                    RaisePropertyChanged("Leader");
                }
            }
        }

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
                    RaisePropertyChanged("Goals");
                }
            }
        }

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
                    RaisePropertyChanged("Selected_Size");
                }
            }
        }
    }
}
