using CharacterManager.ViewModels.TreeViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Events.EventContainers
{
    struct NewEntityRequestContainer
    {
        public NewEntityRequestContainer(OrganizationTreeItemViewModel Source, string Type)
        {
            EventSource = Source;
            RequestType = Type;
        }
        public OrganizationTreeItemViewModel EventSource { get; set; }
        public string RequestType { get; set; }
    }
}
