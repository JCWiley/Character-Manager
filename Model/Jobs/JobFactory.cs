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
    }
}
