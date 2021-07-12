using CharacterManager.Model.Items;
using CharacterManager.Model.RedundantTree;
using Prism.Mvvm;
using System.Collections.ObjectModel;

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

        private IRTreeMember<IEntity> leader;
        public IRTreeMember<IEntity> Leader
        {
            get
            {
                return leader;
            }
            set
            {
                if (leader != value)
                {
                    leader = value;
                    RaisePropertyChanged( nameof( Leader ) );
                }
            }
        }

        private string goals;
        public string Goals
        {
            get
            {
                return goals;
            }
            set
            {
                if (goals != value)
                {
                    goals = value;
                    RaisePropertyChanged( nameof( Goals ) );
                }
            }
        }

        private int selected_size;
        public int Selected_Size
        {
            get
            {
                return selected_size;
            }
            set
            {
                if (selected_size != value)
                {
                    selected_size = value;
                    RaisePropertyChanged( nameof( Selected_Size ) );
                }
            }
        }
    }
}
