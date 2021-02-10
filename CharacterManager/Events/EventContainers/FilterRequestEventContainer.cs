using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Events.EventContainers
{
    struct FilterRequestEventContainer
    {
        public FilterRequestEventContainer(FilterType filterType, string filtercontent)
        {
            FilterType = filterType;
            FilterContent = filtercontent;
        }

        public FilterType FilterType { get; set; }
        public string FilterContent { get; set; }
    }
}
