using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Xml.Serialization;
using System.Runtime.Serialization;


namespace Character_Manager.Model.Entities
{
    [DataContract(Name = "Character", Namespace = "Character_Manager")]
    public class Character : Entity,IEntity
    {
        #region Constructors
        public Character()
        {
            Name = "Default Character";
            alias = "";
            occupation = "";
            birthplace = "";
        }
        #endregion

        #region Data_Members
        [DataMember(Name = "alias")]
        private string alias;
        public string Alias
        {
            get
            {
                return this.alias;
            }
            set
            {
                if (this.alias != value)
                {
                    this.alias = value;
                    this.NotifyPropertyChanged("Alias");
                }
            }
        }

        [DataMember(Name = "occupation")]
        private string occupation;
        public string Occupation
        {
            get
            {
                return this.occupation;
            }
            set
            {
                if (this.occupation != value)
                {
                    this.occupation = value;
                    this.NotifyPropertyChanged("Occupation");
                }
            }
        }

        [DataMember(Name = "birthplace")]
        private string birthplace;
        public string BirthPlace
        {
            get
            {
                return this.birthplace;
            }
            set
            {
                if (this.birthplace != value)
                {
                    this.birthplace = value;
                    this.NotifyPropertyChanged("BirthPlace");
                }
            }
        }
        #endregion
    }
}


#region Constructors

#endregion

#region Property_Handelers

#endregion

#region Functions

#endregion

#region Tree_Members

#endregion

#region Utility_Members

#endregion

#region Data_Members

#endregion