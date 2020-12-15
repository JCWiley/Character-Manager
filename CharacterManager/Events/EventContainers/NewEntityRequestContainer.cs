using CharacterManager.Model.Entities;
using CharacterManager.ViewModels.TreeViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Events.EventContainers
{
    struct NewEntityRequestContainer
    {
        public NewEntityRequestContainer(OrganizationTreeItemViewModel Source, EntityTypes Type)
        {
            EventSource = Source;
            EntityType = Type;
        }
        public OrganizationTreeItemViewModel EventSource { get; set; }
        public EntityTypes EntityType { get; set; }
    }
}
