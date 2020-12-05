using CharacterManager.Model.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;
using Prism.Mvvm;

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
            job_id = new Guid();
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
                    RaisePropertyChanged("Inventory");
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
                    RaisePropertyChanged("Name");
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
                    RaisePropertyChanged("Description");
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
                    RaisePropertyChanged("Quirks");
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
                    RaisePropertyChanged("Location");
                    RaisePropertyChanged("Locations");
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
                    RaisePropertyChanged("Race");
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
                    RaisePropertyChanged("Leader");
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
                    RaisePropertyChanged("Goals");
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
                    RaisePropertyChanged("Selected_Size");
                }
            }
        }

        [DataMember(Name = "job_id")]
        private Guid job_id;
        public Guid Job_ID
        {
            get
            {
                return this.job_id;
            }
        }
    }
}
