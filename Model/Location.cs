using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager
{
    public class Location : INotifyPropertyChanged
    {
        public Location()
        {
            name = "";
        }

        public Location(string I_Name)
        {
            Name = I_Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

            NotifyFieldIsDirty();
        }

        public static event EventHandler FieldIsDirty;
        public void NotifyFieldIsDirty()
        {
            FieldIsDirty?.Invoke(this, new EventArgs());
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
    }
}
