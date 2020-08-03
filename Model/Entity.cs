using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Windows.Media;

namespace Character_Manager
{
    [XmlInclude(typeof(Organization))]
    [XmlInclude(typeof(Character))]
    public abstract class Entity : INotifyPropertyChanged
    {
        static Entity()
        {
            locations = new ObservableCollection<string>();
        }
        public Entity()
        {
            name = "";
            quirks = "{}";
            description = "{}";
            location = "";
            race = "";
            visible = "Black";

            treeheadflag = false;

            gid = Guid.NewGuid();
        }
        public Entity(Guid creatorguid)
        {
            name = "";
            quirks = "{}";
            description = "{}";
            location = "";
            race = "";
            visible = "Black";

           

            if (creatorguid != Guid.Empty)
            {
                ParentGuids.Add(creatorguid);

                location = DataModel.Entities[creatorguid].location;
            }
            else
            {
                treeheadflag = true;
            }
            gid = Guid.NewGuid();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propName)
        {
            this.NotifyFieldIsDirty();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public static event EventHandler DMJobEventOccured;
        public void NotifyDMJobEventOccured()
        {
            DMJobEventOccured?.Invoke(this, new EventArgs());
            NotifyPropertyChanged("Events_Summary");
        }
        private void HandleJobNotification(object sender, EventArgs e)
        {
            this.NotifyDMJobEventOccured();
        }
        public static event EventHandler FieldIsDirty;
        public void NotifyFieldIsDirty()
        {
            FieldIsDirty?.Invoke(this, new EventArgs());
        }

        protected Guid gid;
        public Guid Gid
        {
            get
            {
                return gid;
            }
            set
            {
                if (this.gid != value)
                {
                    this.gid = value;
                    this.NotifyPropertyChanged("Gid");
                }
            }
        }

        protected List<Guid> parentguids = new List<Guid>();
        public  List<Guid> ParentGuids
        {
            get
            {
                return this.parentguids;
            }
            set
            {
                if (this.parentguids != value)
                {
                    this.parentguids = value;
                    this.NotifyPropertyChanged("ParentGuids");
                    this.NotifyPropertyChanged("Entities");
                    this.NotifyPropertyChanged("EntitiesBind");
                }
            }
        }

        [XmlIgnoreAttribute]
        public Entities_Collection ParentEntities
        {
            get
            {
                Entities_Collection temp = new Entities_Collection();
                foreach (Guid x in ParentGuids)
                {
                    temp.Add(DataModel.Entities[x]);
                }
                return temp;
            }
        }

        private bool treeheadflag;
        public bool TreeHeadFlag
        {
            get
            {
                return treeheadflag;
            }
            set
            {
                if (this.treeheadflag != value)
                {
                    this.treeheadflag = value;
                    this.NotifyPropertyChanged("TreeHeadFlag");
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
                    this.NotifyPropertyChanged("Member_List");
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
                    this.NotifyPropertyChanged("Quirks");
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
                    if(!Locations.Contains(value))
                    {
                        Locations.Add(value);
                    }
                    this.NotifyPropertyChanged("Location");
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
                    this.NotifyPropertyChanged("Race");
                }
            }
        }


        private static ObservableCollection<String> locations;

        [XmlIgnoreAttribute]
        public ObservableCollection<String> Locations
        {
            get
            {
                return Entity.locations;
            }
            set
            {
                if (Entity.locations != value)
                {
                    Entity.locations = value;
                    this.NotifyPropertyChanged("Locations");
                }
            }
        }

        [XmlIgnoreAttribute]
        public Entities_Collection Member_List
        {

            get
            {
                Entities_Collection temp = new Entities_Collection();
                if (this is Organization organization)
                {
                    temp.Add(this);
                    foreach (Entity E in organization.Entities)
                    {
                        foreach (Entity I in E.Member_List)
                        {
                            temp.Add(I);
                        }
                    }
                }
                else if (this is Character)
                {
                    temp.Add(this);
                }
                return temp;
            }
        }

        [XmlIgnoreAttribute]
        public ListCollectionView Jobs
        {
            get
            {
                ListCollectionView VS = new ListCollectionView(DataModel.Jobs)
                {
                    Filter = f => (f as Job).Owner_Entity == gid
                };
                return VS;
            }
        }
        [XmlIgnoreAttribute]
        public Events_Collection Events_Summary
        {
            get
            {
                Events_Collection summary = new Events_Collection();
                foreach (Job J in Jobs)
                {
                    foreach (Job_Event JE in J.EC)
                    {
                        summary.Add(JE);
                    }
                }
                return summary;
            }
        }

        public Job AddJob()
        {
            Job J = new Job()
            {
                Owner_Entity = gid,
                StartDate = DataModel.CurrentDay, 
            };
            DataModel.Jobs.Add(J);
            this.NotifyPropertyChanged("Jobs");
            return J;
        }
        public void AdvanceDay()
        {
            foreach (Job J in Jobs)
            {
                J.AdvanceDay();
            }
        }

        private string visible;
        public string Visible
        {
            get
            {
                return this.visible;
            }
            set
            {
                if (this.visible != value)
                {
                    this.visible = value;
                    //if(this is Organization O)
                    //{
                    //    O.NotifyPropertyChanged("Entities");
                    //}
                    this.NotifyPropertyChanged("Visible");
                }
            }
        }

    }
}
