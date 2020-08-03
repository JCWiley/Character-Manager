using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Character_Manager
{
    
    public class Organization : Entity
    {
        public Organization()
        {
            Name = "Default Organization";
        }
        public Organization(Guid creatorguid) : base(creatorguid)
        {
            Name = "Default Organization";
        }


        protected List<Guid> childguids = new List<Guid>();
        public List<Guid> ChildGuids
        {
            get
            {
                return this.childguids;
            }
            set
            {
                if (this.childguids != value)
                {
                    this.childguids = value;
                    this.NotifyPropertyChanged("ChildGuids");
                    this.NotifyPropertyChanged("Entities");
                    this.NotifyPropertyChanged("EntitiesBind");
                }
            }
        }
        [XmlIgnoreAttribute]
        public Entities_Collection Entities
        {
            get
            {
                Entities_Collection temp = new Entities_Collection();
                foreach (Guid x in ChildGuids)
                {
                    //if(MasterEntityCollection[x].Visible)
                    //{
                    //    temp.Add(MasterEntityCollection[x]);
                    //}
                    temp.Add(DataModel.Entities[x]);
                }
                return temp;
            }
        }

        private Entity leader;
        public Entity Leader
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
                    this.NotifyPropertyChanged("Leader");
                }
            }
        }

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
                    this.NotifyPropertyChanged("Goals");
                }
            }
        }

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
                    this.NotifyPropertyChanged("Selected_Size");
                }
            }
        }

        public void Add_Character()
        {
            Character NewChar = new Character(Gid);
            ChildGuids.Add(NewChar.Gid);
            DataModel.Entities.Add(NewChar.Gid, NewChar);
            this.NotifyPropertyChanged("Entities");
        }
        public void Add_Organization()
        {
            Organization NewChar = new Organization(Gid);
            ChildGuids.Add(NewChar.Gid);
            DataModel.Entities.Add(NewChar.Gid, NewChar);
            this.NotifyPropertyChanged("Entities");
        }
        public void Add_Existing_Entity(Guid inc)
        {
            if(!ChildGuids.Contains(inc))
            {
                DataModel.Entities[inc].ParentGuids.Add(Gid);
                ChildGuids.Add(inc);
            }

            this.NotifyPropertyChanged("Entities");
            this.NotifyPropertyChanged("Member_List");
        }

        public void Remove_Child(Guid inc)
        {
            DataModel.Entities[inc].ParentGuids.Remove(Gid);
            ChildGuids.Remove(inc);

            this.NotifyPropertyChanged("Entities");
            this.NotifyPropertyChanged("Member_List");
        }
    }
}
