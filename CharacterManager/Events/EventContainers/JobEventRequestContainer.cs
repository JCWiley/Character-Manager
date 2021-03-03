﻿using CharacterManager.Model.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Events.EventContainers
{
    struct JobEventRequestContainer
    {
        public JobEventRequestContainer(IJob job, int Effects,bool IsCompleteEvent = false)
        {
            J = job;
            E = Effects;
            Complete = IsCompleteEvent;
        }

        public IJob J { get; set; }
        public int E { get; set; }
        public bool Complete { get; set; }
    }
}
