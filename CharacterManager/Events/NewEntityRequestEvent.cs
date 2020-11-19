using CharacterManager.Events.EventContainers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Events
{
    class NewEntityRequestEvent : PubSubEvent<NewEntityRequestContainer>
    {
    }
}
