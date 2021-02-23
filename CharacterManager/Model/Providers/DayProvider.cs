using CharacterManager.Events;
using CharacterManager.Events.EventContainers;
using CharacterManager.Model.Events;
using CharacterManager.Model.Factories;
using CharacterManager.Model.Helpers;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.Services;
using CharacterManager.ViewModels.Helpers;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.Providers
{
    class DayProvider : IDayProvider
    {
        #region Utility_Members
        IDataService DS;
        IEventAggregator EA;
        IJobLogic JL;

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        #endregion

        public DayProvider(IDataService dataService, IEventAggregator eventAggregator, IJobLogic jobLogic)
        {
            EA = eventAggregator;
            DS = dataService;
            JL = jobLogic;

            EA.GetEvent<AdvanceDayRequestEvent>().Subscribe(AdvanceDayRequestEventExecute);
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
            CurrentDay += days;
            foreach (IJob job in DS.Job_List)
            {
                JL.AdvanceJob(job, days);
            }
            EA.GetEvent<UIUpdateRequestEvent>().Publish(ChangeType.DayAdvanced);
        }
        #endregion

    }
}
