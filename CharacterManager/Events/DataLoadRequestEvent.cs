using Prism.Events;

namespace CharacterManager.Events
{
    class DataLoadRequestEvent : PubSubEvent<LoadRequestTypes>
    {
    }

    enum LoadRequestTypes
    {
        LastFile,
        Dialog
    }
}
