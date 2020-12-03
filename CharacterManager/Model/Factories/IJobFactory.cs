using CharacterManager.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Model.Factories
{
    public interface IJobFactory
    {
        public IJob CreateJob();
    }
}
