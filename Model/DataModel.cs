using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml.Serialization;

namespace Character_Manager
{
    public class DataModel : INotifyPropertyChanged
    {
        static SerializableDictionary<Guid, Entity> masterentitycollection;
        static Job_Collection masterjobcollection;
        static DataModel()
        {
            DataModel.masterentitycollection = new SerializableDictionary<Guid, Entity>();
            DataModel.masterjobcollection = new Job_Collection();
        }

        public DataModel()
        {
            currentday = 0;
            isdirty = false;

            Character.DMJobEventOccured += HandleCharacterEventUpdate;

            Character.FieldIsDirty += SetDirty;
            Job.FieldIsDirty += SetDirty;
            Item.FieldIsDirty += SetDirty;
            Job_Event.FieldIsDirty += SetDirty;
            Location.FieldIsDirty += SetDirty;
            worldname = "Worldname";
        }

        public void InitializeDataModel()
        {
            if(entitiesbind.Count == 0)
            {
                Organization O = new Organization(Guid.Empty)
                {
                    Name = "Default World Name",
                };

                entitiesbind.Add(O);

                Entities.Add(O.Gid, O);
            }
        }

        private bool isdirty;
        [XmlIgnoreAttribute]
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

        static private int currentday;
        static public int CurrentDay
        {
            get
            {
                return DataModel.currentday;
            }
            set
            {
                if (DataModel.currentday != value)
                {
                    DataModel.currentday = value;
                }
            }
        }

        public int SerializeCurrentDay
        {
            get
            {
                return DataModel.currentday;
            }
            set
            {
                if (DataModel.currentday != value)
                {
                    DataModel.currentday = value;
                    this.NotifyPropertyChanged("CurrentDay");
                    this.NotifyPropertyChanged("SerializeCurrentDay");
                }
            }
        }

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
        static public Job_Collection Jobs
        {
            get
            {
                return DataModel.masterjobcollection;
            }
            set
            {
                if (DataModel.masterjobcollection != value)
                {
                    DataModel.masterjobcollection = value;
                }
            }
        }
        static public SerializableDictionary<Guid, Entity> Entities
        {
            get
            {
                return DataModel.masterentitycollection;
            }
            set
            {
                if (DataModel.masterentitycollection != value)
                {
                    DataModel.masterentitycollection = value;
                }
            }
        }

        public Job_Collection Jobs_Serialize
        {
            get
            {
                return DataModel.masterjobcollection;
            }
            set
            {
                if (DataModel.masterjobcollection != value)
                {
                    DataModel.masterjobcollection = value;
                }
            }
        }
        public SerializableDictionary<Guid, Entity> Entities_Serialize
        {
            get
            {
                return DataModel.masterentitycollection;
            }
            set
            {
                if (DataModel.masterentitycollection != value)
                {
                    DataModel.masterentitycollection = value;
                }
            }
        }

        private Entities_Collection entitiesbind = new Entities_Collection();
        public Entities_Collection EntitiesBind
        {
            get
            {
                return this.entitiesbind;
            }
            set
            {
                if (this.entitiesbind != value)
                {
                    this.entitiesbind = value;
                    this.NotifyPropertyChanged("Entities");
                    this.NotifyPropertyChanged("EntitiesBind");
                }
            }
        }
   
        public void SetEqual(DataModel inc)
        {
            //Entities.Clear();
            //foreach(Entity E in inc.entities.Values)
            //{
            //    Entities.Add(E.Gid,E);
            //}
            EntitiesBind = inc.EntitiesBind;
            SerializeCurrentDay = inc.SerializeCurrentDay;
            IsDirty = false;
            //CurrentDay = DataModel.CurrentDay;
            Worldname = inc.Worldname;
            //LeadGuid = inc.LeadGuid;
            this.NotifyPropertyChanged("Characters");
        }
        [XmlIgnoreAttribute]
        public Events_Collection Events_Summary
        {
            get
            {
                Events_Collection summary = new Events_Collection();
                foreach(Entity E in Entities.Values)
                {
                    foreach(Job_Event JE in E.Events_Summary)
                    {
                        summary.Add(JE);
                    }
                }
                return summary;
            }
        }

        public void AdvanceDay()
        {
            currentday += 1;
            this.NotifyPropertyChanged("CurrentDay");
            this.NotifyPropertyChanged("SerializeCurrentDay");

            foreach (Entity C in Entities.Values)
            {
                C.AdvanceDay();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                isdirty = true;
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
                
        }
        private void HandleCharacterEventUpdate(object sender, EventArgs e)
        {
            NotifyPropertyChanged("Events_Summary");
        }

        private void SetDirty(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        public void FilterTree(string type,string text)
        {
            switch (type)
            {
                case "Clear":
                    FilterClear();
                    this.NotifyPropertyChanged("EntitiesBind");
                    break;
                default:
                    FilterClear();
                    foreach (Entity E in Entities.Values)
                    {
                        try
                        {
                            string value = (string)E.GetType().GetProperty(type).GetValue(E, null);
                            if (value.Contains(text))
                            {
                                E.Visible = true;
                                SetVisible(E);
                            }
                            else
                            {
                                E.Visible = false;
                            }
                        }
                        catch
                        {

                        }
                    }
                    break;
            }
            //this.NotifyPropertyChanged("Entities");
            this.NotifyPropertyChanged("EntitiesBind");
            //this.NotifyPropertyChanged("Member_List");
        }

        private void FilterClear()
        {
            foreach(Entity E in Entities.Values)
            {
                E.Visible = true;
            }
        }
        private void SetVisible(Entity Parent)
        {
            Parent.Visible = true;
            foreach(Entity E in Parent.ParentEntities)
            {
                SetVisible(E);
            }
        }
    }
}
