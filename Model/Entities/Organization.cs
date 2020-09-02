using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Character_Manager.Model.Collection_Classes;

namespace Character_Manager.Model.Entities
{
    [DataContract(Name = "Organization", Namespace = "Character_Manager")]
    public class Organization : Entity, IEntity
    {
        #region Constructors
        public Organization()
        {
            Name = "Default Organization";
        }
        #endregion 

        //#region Property_Handelers

        //#endregion

        //#region Functions


        //#endregion

        //#region Tree_Members


        //#endregion

        //#region Utility_Members

        //#endregion

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
                    //this.NotifyPropertyChanged("Leader");
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
                    //this.NotifyPropertyChanged("Goals");
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
                    //this.NotifyPropertyChanged("Selected_Size");
                }
            }
        }
        #endregion
    }
}
