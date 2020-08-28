using Character_Manager.Model.Collection_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager.Model.Jobs
{
    public interface IJob
    {
        public int DaysRemaining { get;}
        public int EndDate { get; }

        #region Data_Members
        public Item_Collection Required_Items { get; set; }

        public bool Complete { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public bool IsCurrentlyProgressing { get; set; }

        public int Recurring { get; set; }

        public int Duration { get; set; }

        public int Progress { get; set; }

        public int StartDate { get; set; }

        public int SuccessChance { get; set; }

        public int FailureChance { get; set; }
        #endregion
    }
}
