using CharacterManager.Model.Jobs;

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
