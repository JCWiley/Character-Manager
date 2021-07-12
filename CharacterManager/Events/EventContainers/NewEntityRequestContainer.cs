using CharacterManager.Model.Entities;
using CharacterManager.ViewModels.TreeViewModels;

namespace CharacterManager.Events.EventContainers
{
    struct NewEntityRequestContainer
    {
        public NewEntityRequestContainer(OrganizationTreeItemViewModel Source, EntityTypes Type)
        {
            EventSource = Source;
            EntityType = Type;
        }
        public OrganizationTreeItemViewModel EventSource
        {
            get; set;
        }
        public EntityTypes EntityType
        {
            get; set;
        }
    }
}
