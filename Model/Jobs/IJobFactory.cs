using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager.Model.Jobs
{
    public interface IJobFactory
    {
        public IJob CreateJob();

        public IJobEvent CreateJobEvent(string I_Event_Type, string I_Comment, int I_Day, string I_Character, string I_Job, int I_Progress_Effects);
    }
}
