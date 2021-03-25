using CharacterManager.Model.Events;
using CharacterManager.Model.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.Helpers
{
    public interface IJobLogic
    {
        public void AdvanceJob(IJob job, int days);
        public void ProgressJob(IJob job, int progress);
    }
}
