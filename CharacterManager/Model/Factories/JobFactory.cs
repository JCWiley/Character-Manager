using CharacterManager.Model.Jobs;
using CharacterManager.Model.Providers;

namespace CharacterManager.Model.Factories
{
    public class JobFactory : IJobFactory
    {
        readonly IDayProvider DP;
        public JobFactory(IDayProvider dayProvider)
        {
            DP = dayProvider;
        }
        public IJob CreateJob()
        {
            return new Job( DP.CurrentDay );
        }
    }
}
