using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Character_Manager.Model.Jobs;
using Character_Manager.Model.Factory;
using Character_Manager.Model.Entities;
using Character_Manager.Model.RedundantTree;
using Character_Manager.GeneralInterfaces;

namespace Character_Manager
{
    [DataContract(Name = "DataModel", Namespace = "Character_Manager")]
    public class DataModel : INotifyPropertyChanged
    {
        #region Constructors
        public DataModel(IMessageSender i_Messenger)
        {
            currentday = 0;
            worldname = "Worldname";

            messenger = i_Messenger;

            rfact_entity = new RTreeFactory<IEntity>();
            entitytree = rfact_entity.CreateRTree();

            rfact_job = new RTreeFactory<IJob>();
            jobtree = rfact_job.CreateRTree();

            isdirty = false;

            //Character.DMJobEventOccured += HandleCharacterEventUpdate;

            

            //entities = new Dictionary<Guid, Entity>();
            //jobs = new Job_Collection();
            //entitieshead = new Entities_Collection();

            //Organization O = new Organization(Guid.Empty, this)
            //{
            //    Name = "Default World Name",
            //    TreeHeadFlag = true
            //};
            //EntitiesHead.Add(O);
            //Entities.Add(O.Gid, O);
        }
        #endregion

        #region Property_Handelers
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                isdirty = true;
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }

        }

        //private void HandleCharacterEventUpdate(object sender, EventArgs e)
        //{
        //    NotifyPropertyChanged("Events_Summary");
        //}
        #endregion

        //#region Functions
        //public void SetEqual(DataModel inc)
        //{
        //    //Tree Members
        //    Jobs = inc.Jobs;
        //    foreach(Job J in Jobs)
        //    {
        //        J.DM = this;
        //    }
        //    Entities = inc.Entities;
        //    foreach(Entity E in Entities.Values)
        //    {
        //        E.DM = this;
        //    }
        //    EntitiesHead = inc.EntitiesHead;

        //    //Data Members
        //    CurrentDay = inc.CurrentDay;
        //    Worldname = inc.Worldname;

        //    this.NotifyPropertyChanged("DM");

        //    IsDirty = false;
        //}
        //public void AdvanceDay(int DaysToAdvance)
        //{
        //    for (int i = 0; i < DaysToAdvance; i++)
        //    {
        //        CurrentDay += 1;

        //        foreach (Job J in Jobs)
        //        {
        //            J.AdvanceDay();
        //        }
        //        //foreach (Entity E in Entities.Values) //not needed currently, as entities have no day dependant data
        //        //{
        //        //    E.AdvanceDay();
        //        //}
        //    }

        //    this.NotifyPropertyChanged("CurrentDay");
        //    this.NotifyPropertyChanged("SerializeCurrentDay");
        //}
        //public void FilterTree(string type, string text)
        //{
        //    switch (type)
        //    {
        //        case "Clear":
        //            FilterClear();
        //            this.NotifyPropertyChanged("EntitiesBind");
        //            break;
        //        default:
        //            FilterClear();
        //            foreach (Entity E in Entities.Values)
        //            {
        //                try
        //                {
        //                    string value = (string)E.GetType().GetProperty(type).GetValue(E, null);
        //                    if (value.Contains(text))
        //                    {
        //                        E.Visible = true;
        //                        SetVisible(E);
        //                    }
        //                    else
        //                    {
        //                        E.Visible = false;
        //                    }
        //                }
        //                catch
        //                {

        //                }
        //            }
        //            break;
        //    }
        //    //this.NotifyPropertyChanged("Entities");
        //    this.NotifyPropertyChanged("EntitiesBind");
        //    //this.NotifyPropertyChanged("Member_List");
        //}
        //private void FilterClear()
        //{
        //    foreach (Entity E in Entities.Values)
        //    {
        //        E.Visible = true;
        //    }
        //}
        //private void SetVisible(Entity Parent)
        //{
        //    Parent.Visible = true;
        //    foreach (Entity E in Parent.ParentEntities)
        //    {
        //        SetVisible(E);
        //    }
        //}
        //#endregion

        #region Tree_Members
        private RTreeFactory<IEntity> rfact_entity;
        
        private RTree<IEntity> entitytree;
        public RTree<IEntity> EntityTree
        {
            get
            {
                return entitytree;
            }
            set
            {
                if (this.entitytree != value)
                {
                    this.entitytree = value;
                    //NotifyPropertyChanged for dependant values here
                    this.NotifyPropertyChanged("EntityTree");
                }
            }
        }

        private RTreeFactory<IJob> rfact_job;
        private RTree<IJob> jobtree;
        public RTree<IJob> Jobtree
        {
            get
            {
                return jobtree;
            }
            set
            {
                if (this.jobtree != value)
                {
                    this.jobtree = value;
                    //NotifyPropertyChanged for dependant values here
                    this.NotifyPropertyChanged("jobtree");
                }
            }
        }

        //[DataMember(Name = "jobs")]
        //private Job_Collection jobs;
        //public Job_Collection Jobs
        //{
        //    get
        //    {
        //        return jobs;
        //    }
        //    set
        //    {
        //        if (this.jobs != value)
        //        {
        //            this.jobs = value;
        //            this.NotifyPropertyChanged("Jobs");
        //        }
        //    }
        //}

        //[DataMember(Name = "entities")]
        //private Dictionary<Guid, Entity> entities;
        //public Dictionary<Guid, Entity> Entities
        //{
        //    get
        //    {
        //        return entities;
        //    }
        //    set
        //    {
        //        if (this.entities != value)
        //        {
        //            this.entities = value;
        //            this.NotifyPropertyChanged("Entities");
        //        }
        //    }
        //}

        //[DataMember(Name = "entitieshead")]
        //private Entities_Collection entitieshead;
        //public Entities_Collection EntitiesHead
        //{
        //    get
        //    {
        //        return entitieshead;
        //    }
        //    set
        //    {
        //        if (this.entitieshead != value)
        //        {
        //            this.entitieshead = value;
        //            this.NotifyPropertyChanged("EntitiesHead");
        //        }
        //    }
        //}

        #endregion

        #region Utility_Members
        private bool isdirty;
        public bool IsDirty
        {
            get
            {
                return this.isdirty;
            }
            set
            {
                if (this.isdirty != value)
                {
                    this.isdirty = value;
                }
            }
        }

        private IMessageSender messenger;
        //public Events_Collection Events_Summary
        //{
        //    get
        //    {
        //        Events_Collection summary = new Events_Collection();
        //        foreach (Entity E in Entities.Values)
        //        {
        //            foreach (Job_Event JE in E.Events_Summary)
        //            {
        //                summary.Add(JE);
        //            }
        //        }
        //        return summary;
        //    }
        //}
        #endregion

        #region Data_Members
        [DataMember(Name = "currentday")]
        private int currentday;
        public int CurrentDay
        {
            get
            {
                return this.currentday;
            }
            set
            {
                if (this.currentday != value)
                {
                    this.currentday = value;
                    this.NotifyPropertyChanged("CurrentDay");
                }
            }
        }

        [DataMember(Name = "worldname")]
        private string worldname;
        public string Worldname
        {
            get
            {
                return this.worldname;
            }
            set
            {
                if (this.worldname != value)
                {
                    this.worldname = value;
                    this.NotifyPropertyChanged("Worldname");
                }
            }
        }
        #endregion
    }
}
