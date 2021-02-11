using CharacterManager.Events;
using CharacterManager.Events.EventContainers;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.ViewModels.TreeViewModels
{
    public class EntityFilterViewModel : BindableBase
    {
        private readonly IEventAggregator EA;
        public EntityFilterViewModel(IEventAggregator eventAggregator)
        {
            EA = eventAggregator;
        }

        #region Variables
        private string filtercontent;

        public string FilterContent
        {
            get 
            {
                return filtercontent;
            }
            set
            {
                if(filtercontent != value)
                {
                    filtercontent = value;
                    RaisePropertyChanged(nameof(FilterContent));
                }
            }
        }

        private FilterType filterselection;

        public FilterType FilterSelection
        {
            get
            {
                return filterselection;
            }
            set
            {
                if (filterselection != value)
                {
                    filterselection = value;
                    RaisePropertyChanged(nameof(FilterSelection));
                }
            }
        }
        #endregion

        #region BindingTargets
        public static IEnumerable<FilterType> FilterTypeList
        {
            get
            {
                return Enum.GetValues(typeof(FilterType)).Cast<FilterType>();
            }
        }
        #endregion

        #region Commands
        private DelegateCommand _commandclearfilter;
        private DelegateCommand _commandapplyfilter;

        public DelegateCommand CommandClearFilter => _commandclearfilter ??= new DelegateCommand(CommandClearFilterExecute);
        public DelegateCommand CommandApplyFilter => _commandapplyfilter ??= new DelegateCommand(CommandApplyFilterExecute);
        #endregion

        #region Command Handlers
        private void CommandClearFilterExecute()
        {
            FilterContent = "";
            EA.GetEvent<FilterClearRequestEvent>().Publish("");
        }
        private void CommandApplyFilterExecute()
        {
            EA.GetEvent<FilterRequestEvent>().Publish(new FilterRequestEventContainer(FilterSelection, FilterContent));
        }
        #endregion
    }
}
