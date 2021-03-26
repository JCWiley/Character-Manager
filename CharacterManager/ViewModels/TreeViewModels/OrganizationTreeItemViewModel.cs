using CharacterManager.Events;
using CharacterManager.Events.EventContainers;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Factories;
using CharacterManager.Model.RedundantTree;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace CharacterManager.ViewModels.TreeViewModels
{
    public class OrganizationTreeItemViewModel : BindableBase
    {
        public OrganizationTreeItemViewModel(IRTreeMember<IEntity> Parent,IRTreeMember<IEntity> target,ITreeItemViewModelFactory treeItemViewModelFactory, IEventAggregator eventAggregator)
        {
            Target = target;
            parent = Parent;
            TreeItemViewModelFactory = treeItemViewModelFactory;

            Visible = true;
            IsSelected = false;
            IsExpanded = true;

            EA = eventAggregator;

            EA.GetEvent<FilterClearRequestEvent>().Subscribe(FilterClearRequestEventExecute);

            RebuildChildren();
        }


        #region Variables
        private readonly IEventAggregator EA;
        private readonly ITreeItemViewModelFactory TreeItemViewModelFactory;

        private IRTreeMember<IEntity> parent;

        private IRTreeMember<IEntity> target;
        public IRTreeMember<IEntity> Target
        {
            get { return target; }
            set { SetProperty(ref target, value); }
        }

        public Organization Org
        {
            get
            {
                return (Organization)Target.Item;
            }
            set
            {
                Target.Item = value;
                RaisePropertyChanged(nameof(Org));
            }
        }

        private ObservableCollection<object> children;

        public ObservableCollection<object> Children
        {
            get { return children; }
            set { SetProperty(ref children, value); }
        }
        #endregion
        #region Functions
        public void RebuildChildren()
        {
            children = new ObservableCollection<object>();

            foreach (IRTreeMember<IEntity> E in Target.Child_Items)
            {
                if (E.Item is Organization)
                {
                    children.Add(TreeItemViewModelFactory.CreateOrganizationViewModel(Target,E));
                }
                else if (E.Item is Character)
                {
                    children.Add(TreeItemViewModelFactory.CreateCharacterViewModel(Target,E));
                }
                else
                {
                    throw new Exception("IEntity is not Character or Organization");
                }
            }
            RaisePropertyChanged(nameof(Children));
        }

        public bool ApplyFilter(FilterRequestEventContainer filter)
        {
            bool Should_Be_Visible = true;
            switch (filter.FilterType)
            {
                case FilterType.Name:
                    if (Target.Item.Name.Contains(filter.FilterContent))
                    {
                        Should_Be_Visible = true;
                    }
                    else
                    {
                        Should_Be_Visible = false;
                    }
                    break;
                case FilterType.Race:
                    if (Target.Item.Race.Contains(filter.FilterContent))
                    {
                        Should_Be_Visible = true;
                    }
                    else
                    {
                        Should_Be_Visible = false;
                    }
                    break;
                case FilterType.Location:
                    if (Target.Item.Location.Contains(filter.FilterContent))
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

            foreach (object child in Children)
            {
                if (child is OrganizationTreeItemViewModel O)
                {
                    if(O.ApplyFilter(filter) == true)
                    {
                        Should_Be_Visible = true;
                        break;
                    }
                }
                if(child is CharacterTreeItemViewModel C)
                {
                    if(C.ApplyFilter(filter) == true)
                    {
                        Should_Be_Visible = true;
                        break;
                    }
                }
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
                if(isselected == true)
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

        #region Commands

        private DelegateCommand _commandnewcharacter;
        private DelegateCommand _commandneworganization;
        private DelegateCommand _commandcut;
        private DelegateCommand _commandcopy;
        private DelegateCommand _commandpaste;
        private DelegateCommand _commanddelete;

        public DelegateCommand CommandNewCharacter => _commandnewcharacter ??= new DelegateCommand(CommandNewCharacterExecute);
        public DelegateCommand CommandNewOrganization => _commandneworganization ??= new DelegateCommand(CommandNewOrganizationExecute);
        public DelegateCommand CommandCut => _commandcut ??= new DelegateCommand(CommandCutExecute);
        public DelegateCommand CommandCopy => _commandcopy ??= new DelegateCommand(CommandCopyExecute);
        public DelegateCommand CommandPaste => _commandpaste ??= new DelegateCommand(CommandPasteExecute);
        public DelegateCommand CommandDelete => _commanddelete ??= new DelegateCommand(CommandDeleteExecute);
        #endregion

        #region Command Handlers
        private void CommandNewCharacterExecute()
        {
            EA.GetEvent<NewEntityRequestEvent>().Publish(new Events.EventContainers.NewEntityRequestContainer(this,EntityTypes.Character));
        }
        private void CommandNewOrganizationExecute()
        {
            EA.GetEvent<NewEntityRequestEvent>().Publish(new Events.EventContainers.NewEntityRequestContainer(this,EntityTypes.Organization));
        }
        private void CommandCutExecute()
        {
            EA.GetEvent<AlterEntityRelationshipsEvent>().Publish(new AlterEntityRelationshipContainer(RelationshipChangeType.Cut, parent, Target));
        }
        private void CommandCopyExecute()
        {
            EA.GetEvent<AlterEntityRelationshipsEvent>().Publish(new AlterEntityRelationshipContainer(RelationshipChangeType.Copy, parent, Target));
        }
        private void CommandPasteExecute()
        {
            EA.GetEvent<AlterEntityRelationshipsEvent>().Publish(new AlterEntityRelationshipContainer(RelationshipChangeType.Paste, parent, Target));
        }
        private void CommandDeleteExecute()
        {
            EA.GetEvent<AlterEntityRelationshipsEvent>().Publish(new AlterEntityRelationshipContainer(RelationshipChangeType.DeleteLocal, parent, Target));
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