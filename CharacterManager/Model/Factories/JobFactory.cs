using CharacterManager.Model.Interfaces;
using CharacterManager.Model.Jobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Model.Factories
{
    public class JobFactory : IJobFactory
    {
        public IJob CreateJob()
        {
            return new Job();
        }
    }
}
