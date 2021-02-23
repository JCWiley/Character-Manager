using CharacterManager.Model.Events;
using CharacterManager.Model.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Events.EventContainers
{
    struct JobEventOccuredContainer
    {
        public JobEventOccuredContainer(IJob targetjob, IEvent newevent)
        {
            TargetJob = targetjob;
            NewEvent = newevent;
        }

        public IJob TargetJob { get; set; }
        public IEvent NewEvent {get;set;}
    }
}
