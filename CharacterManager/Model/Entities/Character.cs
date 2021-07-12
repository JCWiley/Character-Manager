using CharacterManager.Model.Items;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace CharacterManager.Model.Entities
{

    public class Character : BindableBase, IEntity
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
                return inventory;
            }
            set
            {
                if (inventory != value)
                {
                    inventory = value;
                    RaisePropertyChanged( nameof( Inventory ) );
                }
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged( nameof( Name ) );
                }
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (description != value)
                {
                    description = value;
                    RaisePropertyChanged( nameof( Description ) );
                }
            }
        }

        private string quirks;
        public string Quirks
        {
            get
            {
                return quirks;
            }
            set
            {
                if (quirks != value)
                {
                    quirks = value;
                    RaisePropertyChanged( nameof( Quirks ) );
                }
            }
        }

        private string location;
        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                if (location != value)
                {
                    location = value;
                    RaisePropertyChanged( nameof( Location ) );
                    RaisePropertyChanged( "Locations" );
                }
            }
        }

        private string race;
        public string Race
        {
            get
            {
                return race;
            }
            set
            {
                if (race != value)
                {
                    race = value;
                    RaisePropertyChanged( nameof( Race ) );
                }
            }
        }

        private string alias;
        public string Alias
        {
            get
            {
                return alias;
            }
            set
            {
                if (alias != value)
                {
                    alias = value;
                    RaisePropertyChanged( nameof( Alias ) );
                }
            }
        }

        private string occupation;
        public string Occupation
        {
            get
            {
                return occupation;
            }
            set
            {
                if (occupation != value)
                {
                    occupation = value;
                    RaisePropertyChanged( nameof( Occupation ) );
                }
            }
        }

        private string birthplace;
        public string BirthPlace
        {
            get
            {
                return birthplace;
            }
            set
            {
                if (birthplace != value)
                {
                    birthplace = value;
                    RaisePropertyChanged( nameof( BirthPlace ) );
                }
            }
        }
    }
}
