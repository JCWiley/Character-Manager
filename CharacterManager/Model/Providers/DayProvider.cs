using CharacterManager.Events;
using CharacterManager.Model.Helpers;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.Services;
using Prism.Events;
using System;

namespace CharacterManager.Model.Providers
{
    class DayProvider : IDayProvider
    {
        #region Utility_Members
        readonly IDataService DS;
        readonly IEventAggregator EA;
        readonly IJobLogic JL;

        private static readonly Random random = new();
        private static readonly object syncLock = new();
        #endregion

        public DayProvider(IDataService dataService, IEventAggregator eventAggregator, IJobLogic jobLogic)
        {
            EA = eventAggregator;
            DS = dataService;
            JL = jobLogic;

            EA.GetEvent<AdvanceDayRequestEvent>().Subscribe( AdvanceDayRequestEventExecute );
        }
        public int CurrentDay
        {
            get
            {
                return DS.CurrentDay;
            }
            set
            {
                DS.CurrentDay = value;
            }
        }

        #region Event Handlers
        private void AdvanceDayRequestEventExecute(int days)
        {
            //CurrentDay += days;
            for (int i = 0; i < days; i++)
            {
                CurrentDay++;
                foreach (IJob job in DS.Job_List)
                {
                    JL.AdvanceJob( job, 1 );
                }
            }

            EA.GetEvent<UIUpdateRequestEvent>().Publish( ChangeType.DayAdvanced );
        }
        #endregion

    }
}
