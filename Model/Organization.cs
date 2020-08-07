using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Character_Manager
{
    [DataContract(Name = "Organization", Namespace = "Character_Manager")]
    public class Organization : Entity
    {
        #region Constructors
        public Organization(Guid creatorguid, DataModel I_DM) : base(creatorguid, I_DM)
        {
            Name = "Default Organization";
        }
        #endregion

        #region Property_Handelers

        #endregion

        #region Functions
        public void Add_Character()
        {
            Character NewChar = new Character(Gid,DM)
            {
                IsSelected = true
            };
            ChildGuids.Add(NewChar.Gid);
            DM.Entities.Add(NewChar.Gid, NewChar);
            this.NotifyPropertyChanged("Entities");
        }
        public void Add_Organization()
        {
            Organization NewOrg = new Organization(Gid,DM)
            {
                IsSelected = true
            };
            ChildGuids.Add(NewOrg.Gid);
            DM.Entities.Add(NewOrg.Gid, NewOrg);
            this.NotifyPropertyChanged("Entities");
        }
        public void Add_Existing_Entity(Guid inc)
        {
            if (!ChildGuids.Contains(inc))
            {
                DM.Entities[inc].ParentGuids.Add(Gid);
                ChildGuids.Add(inc);
            }

            this.NotifyPropertyChanged("Entities");
            this.NotifyPropertyChanged("Member_List");
        }
        public void Remove_Child(Guid inc)
        {
            DM.Entities[inc].ParentGuids.Remove(Gid);
            ChildGuids.Remove(inc);

            this.NotifyPropertyChanged("Entities");
            this.NotifyPropertyChanged("Member_List");
        }
        #endregion

        #region Tree_Members
        [DataMember(Name = "childguids")]
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
        #endregion

        #region Utility_Members
        public Entities_Collection Entities
        {
            get
            {
                Entities_Collection temp = new Entities_Collection();
                foreach (Guid x in ChildGuids)
                {
                    temp.Add(DM.Entities[x]);
                }
                return temp;
            }
        }
        #endregion

        #region Data_Members
        [DataMember(Name = "leader")]
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

        [DataMember(Name = "goals")]
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

        [DataMember(Name = "selected_size")]
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
        #endregion
    }
}
