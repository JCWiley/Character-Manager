using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager.Model.Jobs
{
    public interface IJobEvent
    {
        #region Data_Members
        public string Event_Type { get; set; }

        public string Comment { get; set; }

        public int Day { get; set; }

        public string Character { get; set; }

        public string Job { get; set; }

        public int Progress_Effects { get; set; }

        #endregion

    }
}
