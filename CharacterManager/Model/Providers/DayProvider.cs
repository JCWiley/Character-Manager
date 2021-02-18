using CharacterManager.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.Providers
{
    class DayProvider : IDayProvider
    {
        IDataService DS;
        public DayProvider(IDataService dataService)
        {
            DS = dataService;
        }
        public int CurrentDay
        {
            get
            {
                return DS.CurrentDay;
            }
        }
    }
}
