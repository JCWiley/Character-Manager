using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager
{
    public class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));

            NotifyFieldIsDirty();
        }

        public static event EventHandler FieldIsDirty;
        public void NotifyFieldIsDirty()
        {
            if (FieldIsDirty != null)
                FieldIsDirty(this, new EventArgs());
        }

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
    }
}
