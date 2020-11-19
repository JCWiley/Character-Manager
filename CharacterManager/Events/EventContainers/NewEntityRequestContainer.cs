using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Events.EventContainers
{
    struct NewEntityRequestContainer
    {
        public NewEntityRequestContainer(Guid Source, string Type)
        {
            EventSource = Source;
            RequestType = Type;
        }
        public Guid EventSource { get; set; }
        public string RequestType { get; set; }
    }
}
