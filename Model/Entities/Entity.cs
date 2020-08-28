using System;
using System.Linq;
using System.ComponentModel;
using System.Runtime.Serialization;
using Character_Manager.Model.Collection_Classes;

namespace Character_Manager.Model.Entities
{

    [DataContract(Name = "Entity", Namespace = "Character_Manager")]
    [KnownType(typeof(Organization))]
    [KnownType(typeof(Character))]
    public abstract class Entity : IEntity
    {
        #region Constructors
        public Entity()
        {
            name = "";
            quirks = "{}";
            description = "{}";
            location = "";
            race = "";

            visible = true;
            IsSelected = false;
            IsExpanded = false;

            inventory = new Item_Collection();
        }
        #endregion

        #region Property_Handelers
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            //DM.IsDirty = true;
        }
        #endregion

        //#region Functions
        //public Job AddJob() //not my responsability, outsource to datamodel
        //{
        //    //Job J = new Job(DM)
        //    //{
        //    //    Owner_Entity = gid,
        //    //    StartDate = DM.CurrentDay,
        //    //};
        //    //DM.Jobs.Add(J);
        //    //this.NotifyPropertyChanged("Jobs");
        //    //return J;
        //}
        //#endregion

        //#region Tree_Members
        //[DataMember(Name = "dm")]
        //private DataModel dm;
        //public DataModel DM
        //{
        //    get
        //    {
        //        return dm;
        //    }
        //    set
        //    {
        //        if (this.dm != value)
        //        {
        //            this.dm = value;
        //            this.NotifyPropertyChanged("DM");
        //        }
        //    }
        //}

        //This code contains datamembers that need to be moved elsewhere
        //[DataMember(Name = "gid")]
        //protected Guid gid;
        //public Guid Gid
        //{
        //    get
        //    {
        //        return gid;
        //    }
        //    set
        //    {
        //        if (this.gid != value)
        //        {
        //            this.gid = value;
        //            this.NotifyPropertyChanged("Gid");
        //        }
        //    }
        //}

        //[DataMember(Name = "parentguids")]
        //protected List<Guid> parentguids = new List<Guid>();
        //public List<Guid> ParentGuids
        //{
        //    get
        //    {
        //        return this.parentguids;
        //    }
        //    set
        //    {
        //        if (this.parentguids != value)
        //        {
        //            this.parentguids = value;
        //            this.NotifyPropertyChanged("ParentGuids");
        //            this.NotifyPropertyChanged("Entities");
        //            this.NotifyPropertyChanged("EntitiesBind");
        //        }
        //    }
        //}

        //[DataMember(Name = "treeheadflag")]
        //private bool treeheadflag;
        //public bool TreeHeadFlag
        //{
        //    get
        //    {
        //        return treeheadflag;
        //    }
        //    set
        //    {
        //        if (this.treeheadflag != value)
        //        {
        //            this.treeheadflag = value;
        //            this.NotifyPropertyChanged("TreeHeadFlag");
        //        }
        //    }
        //}
        //#endregion

        #region Utility_Members
        //private static ObservableCollection<String> locations;
        //public ObservableCollection<String> Locations
        //{
        //    get
        //    {
        //        return Entity.locations;
        //    }
        //    set
        //    {
        //        if (Entity.locations != value)
        //        {
        //            Entity.locations = value;
        //            this.NotifyPropertyChanged("Locations");
        //        }
        //    }
        //}
        //public ListCollectionView Jobs
        //{
        //    get
        //    {
        //        ListCollectionView VS = new ListCollectionView(DM.Jobs)
        //        {
        //            Filter = f => (f as Job).Owner_Entity == gid
        //        };
        //        return VS;
        //    }
        //}
        //public Events_Collection Events_Summary
        //{
        //    get
        //    {
        //        Events_Collection summary = new Events_Collection();
        //        foreach (Job J in Jobs)
        //        {
        //            foreach (Job_Event JE in J.EC)
        //            {
        //                summary.Add(JE);
        //            }
        //        }
        //        return summary;
        //    }
        //}
        #endregion

        #region Filter_Members
        [DataMember(Name = "visible")]
        private bool visible;
        public bool Visible
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
                    this.NotifyPropertyChanged("Visible");
                }
            }
        }

        private bool isselected;
        public bool IsSelected
        {
            get
            {
                return this.isselected;
            }
            set
            {
                if (this.isselected != value)
                {
                    this.isselected = value;
                    this.NotifyPropertyChanged("IsSelected");
                }
            }
        }

        [DataMember(Name = "isexpanded")]
        private bool isexpanded;
        public bool IsExpanded
        {
            get
            {
                return this.isexpanded;
            }
            set
            {
                if (this.isexpanded != value)
                {
                    this.isexpanded = value;
                    this.NotifyPropertyChanged("IsExpanded");
                }
            }
        }
        #endregion

        #region Data_Members
        [DataMember(Name = "inventory")]
        private Item_Collection inventory;
        public Item_Collection Inventory
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
                    this.NotifyPropertyChanged("Inventory");
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
                    this.NotifyPropertyChanged("Member_List");
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
                    this.NotifyPropertyChanged("Quirks");
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
                    //if (!Locations.Contains(value))
                    //{
                    //    Locations.Add(value);
                    //}
                    this.NotifyPropertyChanged("Location");
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
                    this.NotifyPropertyChanged("Race");
                }
            }
        }
        #endregion
    }
}
