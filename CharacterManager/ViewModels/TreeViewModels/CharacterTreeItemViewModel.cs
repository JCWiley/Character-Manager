using CharacterManager.Events;
using CharacterManager.Events.EventContainers;
using CharacterManager.Model.Entities;
using CharacterManager.Model.RedundantTree;
using Prism.Events;
using Prism.Mvvm;
using System;

namespace CharacterManager.ViewModels.TreeViewModels
{
    public class CharacterTreeItemViewModel : BindableBase
    {
        public CharacterTreeItemViewModel(IRTreeMember<IEntity> target, IEventAggregator eventAggregator)
        {
            Target = target;

            EA = eventAggregator;

            EA.GetEvent<FilterRequestEvent>().Subscribe(FilterRequestEventExecute);
            EA.GetEvent<FilterClearRequestEvent>().Subscribe(FilterClearRequestEventExecute);

            Visible = true;
            IsSelected = false;
            IsExpanded = false;

        }



        #region Variables
        private IEventAggregator EA;
        private IRTreeMember<IEntity> Target;

        public Character Char
        {
            get 
            {
                return (Character)Target.Item; 
            }
            set
            {
                Target.Item = value;
                RaisePropertyChanged("Char");
            }
        }
        #endregion

        #region List State Paramaters
        private bool visible;
        public bool Visible
        {
            get
            {
                return this.visible;
            }
            set
            {
                SetProperty(ref visible, value);
            }
        }

        private bool isselected;
        public bool IsSelected
        {
            get
            {
                return this.isselected;
            }
            set
            {
                SetProperty(ref isselected, value);
                if (isselected == true)
                {
                    EA.GetEvent<SelectedEntityChangedEvent>().Publish(Target);
                }
            }
        }

        private bool isexpanded;
        public bool IsExpanded
        {
            get
            {
                return this.isexpanded;
            }
            set
            {
                SetProperty(ref isexpanded, value);
            }
        }
        #endregion

        #region Event Handlers
        private void FilterRequestEventExecute(FilterRequestEventContainer Paramaters)
        {
            switch (Paramaters.FilterType)
            {
                case FilterType.Name:
                    if(Target.Item.Name.Contains(Paramaters.FilterContent))
                    {
                        Visible = true;
                    }
                    else
                    {
                        Visible = false;
                    }
                    break;
                case FilterType.Race:
                    if (Target.Item.Race.Contains(Paramaters.FilterContent))
                    {
                        Visible = true;
                    }
                    else
                    {
                        Visible = false;
                    }
                    break;
                case FilterType.Location:
                    if (Target.Item.Location.Contains(Paramaters.FilterContent))
                    {
                        Visible = true;
                    }
                    else
                    {
                        Visible = false;
                    }
                    break;
                default:
                    break;
            }
        }
        private void FilterClearRequestEventExecute(string Unused)
        {
            Visible = true;
        }
        #endregion
    }
}
