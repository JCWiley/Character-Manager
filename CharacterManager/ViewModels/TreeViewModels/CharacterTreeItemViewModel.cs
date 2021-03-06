﻿using CharacterManager.Events;
using CharacterManager.Events.EventContainers;
using CharacterManager.Model.Entities;
using CharacterManager.Model.RedundantTree;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace CharacterManager.ViewModels.TreeViewModels
{
    public class CharacterTreeItemViewModel : BindableBase
    {
        public CharacterTreeItemViewModel(IRTreeMember<IEntity> Parent, IRTreeMember<IEntity> target, IEventAggregator eventAggregator)
        {
            Target = target;
            parent = Parent;

            EA = eventAggregator;

            EA.GetEvent<FilterClearRequestEvent>().Subscribe( FilterClearRequestEventExecute );

            Visible = true;
            IsSelected = false;
            IsExpanded = false;
        }



        #region Variables
        private readonly IEventAggregator EA;
        private readonly IRTreeMember<IEntity> Target;

        private readonly IRTreeMember<IEntity> parent;
        public Character Char
        {
            get
            {
                return (Character)Target.Item;
            }
            set
            {
                Target.Item = value;
                RaisePropertyChanged( nameof( Char ) );
            }
        }
        #endregion

        #region Functions

        public bool ApplyFilter(FilterRequestEventContainer filter)
        {
            bool Should_Be_Visible = true;
            switch (filter.FilterType)
            {
                case FilterType.Name:
                    if (Target.Item.Name.Contains( filter.FilterContent ))
                    {
                        Should_Be_Visible = true;
                    }
                    else
                    {
                        Should_Be_Visible = false;
                    }
                    break;
                case FilterType.Race:
                    if (Target.Item.Race.Contains( filter.FilterContent ))
                    {
                        Should_Be_Visible = true;
                    }
                    else
                    {
                        Should_Be_Visible = false;
                    }
                    break;
                case FilterType.Location:
                    if (Target.Item.Location.Contains( filter.FilterContent ))
                    {
                        Should_Be_Visible = true;
                    }
                    else
                    {
                        Should_Be_Visible = false;
                    }
                    break;
                default:
                    break;
            }
            Visible = Should_Be_Visible;
            return Should_Be_Visible;
        }

        #endregion

        #region List State Paramaters
        private bool visible;
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                SetProperty( ref visible, value );
            }
        }

        private bool isselected;
        public bool IsSelected
        {
            get
            {
                return isselected;
            }
            set
            {
                SetProperty( ref isselected, value );
                if (isselected == true)
                {
                    EA.GetEvent<SelectedEntityChangedEvent>().Publish( Target );
                }
            }
        }

        private bool isexpanded;
        public bool IsExpanded
        {
            get
            {
                return isexpanded;
            }
            set
            {
                SetProperty( ref isexpanded, value );
            }
        }
        #endregion

        #region Commands
        private DelegateCommand _commandcut;
        private DelegateCommand _commandcopy;
        private DelegateCommand _commanddelete;

        public DelegateCommand CommandCut
        {
            get
            {
                return _commandcut ??= new DelegateCommand( CommandCutExecute );
            }
        }

        public DelegateCommand CommandCopy
        {
            get
            {
                return _commandcopy ??= new DelegateCommand( CommandCopyExecute );
            }
        }

        public DelegateCommand CommandDelete
        {
            get
            {
                return _commanddelete ??= new DelegateCommand( CommandDeleteExecute );
            }
        }
        #endregion

        #region Command Handlers
        private void CommandCutExecute()
        {
            EA.GetEvent<AlterEntityRelationshipsEvent>().Publish( new AlterEntityRelationshipContainer( RelationshipChangeType.Cut, parent, Target ) );
        }
        private void CommandCopyExecute()
        {
            EA.GetEvent<AlterEntityRelationshipsEvent>().Publish( new AlterEntityRelationshipContainer( RelationshipChangeType.Copy, parent, Target ) );
        }
        private void CommandDeleteExecute()
        {
            EA.GetEvent<AlterEntityRelationshipsEvent>().Publish( new AlterEntityRelationshipContainer( RelationshipChangeType.DeleteLocal, parent, Target ) );
        }
        #endregion

        #region Event Handlers
        private void FilterClearRequestEventExecute(string Unused)
        {
            Visible = true;
        }
        #endregion
    }
}
