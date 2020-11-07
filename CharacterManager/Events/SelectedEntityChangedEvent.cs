using CharacterManager.Model.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Events
{
    class SelectedEntityChangedEvent : PubSubEvent<IEntity>
    {
    }
}
