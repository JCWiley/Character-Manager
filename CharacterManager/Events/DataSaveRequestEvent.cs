using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

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
