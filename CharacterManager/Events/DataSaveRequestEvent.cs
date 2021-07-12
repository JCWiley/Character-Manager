using Prism.Events;

namespace CharacterManager.Events
{
    class DataSaveRequestEvent : PubSubEvent<SaveRequestTypes>
    {
    }

    enum SaveRequestTypes
    {
        Save,
        SaveAs
    }
}
