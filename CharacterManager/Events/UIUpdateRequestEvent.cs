using Prism.Events;

namespace CharacterManager.Events
{
    class UIUpdateRequestEvent : PubSubEvent<ChangeType>
    {
    }

    enum ChangeType
    {
        SelectedCharacterChanged,
        SelectedOrganizationChanged,
        EntityListChanged,
        JobEventListChanged,
        JobListChanged,
        DayAdvanced
    }
}
