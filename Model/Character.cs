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

namespace Character_Manager
{
    public class Character : Entity
    {
        public Character()
        {
            Name = "Default Character";
            alias = "";
            occupation = "";
            birthplace = "";
            
        }
        public Character(Guid creatorguid) : base(creatorguid)
        {
            Name = "Default Character";
            alias = "";
            occupation = "";
            birthplace = "";
        }

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



    }
}
