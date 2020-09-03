using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Core.Interfaces
{
    public interface IDayModel
    {
        int Day { get; set; }
        void IncrementDay();
    }
}
