using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

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
