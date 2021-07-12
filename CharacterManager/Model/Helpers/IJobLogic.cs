using CharacterManager.Model.Jobs;

namespace CharacterManager.Model.Helpers
{
    public interface IJobLogic
    {
        public void AdvanceJob(IJob job, int days);
        public void ProgressJob(IJob job, int progress);
    }
}
