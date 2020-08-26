using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Character_Manager.Model.Jobs
{
    public class JobFactory : IJobFactory
    {
        public IJob CreateJob()
        {
            return new Job();
        }

        public IJobEvent CreateJobEvent(string I_Event_Type, string I_Comment, int I_Day, string I_Character, string I_Job, int I_Progress_Effects)
        {
            return new Job_Event(I_Event_Type, I_Comment, I_Day, I_Character, I_Job, I_Progress_Effects);
        }
    }
}
