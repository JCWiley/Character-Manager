﻿using CharacterManager.Events.EventContainers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Events
{
    class JobEventOccuredEvent :PubSubEvent<JobEventOccuredContainer>
    {
    }
}