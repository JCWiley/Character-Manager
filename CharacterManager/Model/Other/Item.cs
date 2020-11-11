﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Character_Manager.Model.Other
{
    [DataContract(Name = "Item", Namespace = "Character_Manager")]
    public class Item
    {
        #region Data_Members
        [DataMember(Name = "aquired")]
        private bool aquired;
        public bool Aquired
        {
            get
            {
                return aquired;
            }
            set
            {
                if (this.aquired != value)
                {
                    this.aquired = value;
                    this.NotifyPropertyChanged("Aquired");
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
                    this.NotifyPropertyChanged("Name");
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
                    this.NotifyPropertyChanged("Location");
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
                    this.NotifyPropertyChanged("Description");
                }
            }
        }

        [DataMember(Name = "weight")]
        private float weight;
        public float Weight
        {
            get
            {
                return this.weight;
            }
            set
            {
                if (this.weight != value)
                {
                    this.weight = value;
                    this.NotifyPropertyChanged("Weight");
                }
            }
        }
        #endregion
    }
}
