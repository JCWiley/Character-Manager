using CharacterManager.Events;
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
        public OrganizationTreeItemViewModel(IRTreeMember<IEntity> target,ITreeItemViewModelFactory treeItemViewModelFactory, IEventAggregator eventAggregator)
        {
            Target = target;
            TreeItemViewModelFactory = treeItemViewModelFactory;

            Visible = true;
            IsSelected = false;
            IsExpanded = false;

            EA = eventAggregator;

            Org = (Organization)Target.Item;

            RebuildChildren();
        }


        #region Variables
        private IEventAggregator EA;
        private ITreeItemViewModelFactory TreeItemViewModelFactory;

        private IRTreeMember<IEntity> target;
        public IRTreeMember<IEntity> Target
        {
            get { return target; }
            set { SetProperty(ref target, value); }
        }

        private Organization org;

        public Organization Org
        {
            get { return org; }
            set { SetProperty(ref org, value); }
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
                    children.Add(TreeItemViewModelFactory.CreateOrganizationViewModel(E));
                }
                else if (E.Item is Character)
                {
                    children.Add(TreeItemViewModelFactory.CreateCharacterViewModel(E));
                }
                else
                {
                    throw new Exception("IEntity is not Character or Organization");
                }
            }
            RaisePropertyChanged(nameof(Children));
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
                    EA.GetEvent<SelectedEntityChangedEvent>().Publish(Org);
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
            EA.GetEvent<NewEntityRequestEvent>().Publish(new Events.EventContainers.NewEntityRequestContainer(this, "Character"));
        }
        private void CommandNewOrganizationExecute()
        {
            EA.GetEvent<NewEntityRequestEvent>().Publish(new Events.EventContainers.NewEntityRequestContainer(this, "Organization"));
        }
        private void CommandCutExecute()
        {
        }
        private void CommandCopyExecute()
        {
        }
        private void CommandPasteExecute()
        {
        }
        private void CommandDeleteExecute()
        {
        }
        #endregion
    }
}
