using CharacterManager.Model.RedundantTree;
using CharacterManager.Events;
using CharacterManager.Model.Interfaces;
using CharacterManager.Model.Entities;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Prism.Ioc;
using Prism.Unity;
using CharacterManager.Model.Factories;
using System.Collections.ObjectModel;

using System.Linq;

namespace CharacterManager.ViewModels.TreeViewModels
{
    public class EntityListViewModel : BindableBase
    {
        private IEventAggregator EA;
        
        private IEntityFactory EF;
        IEntity SelectedEntity;

        public EntityListViewModel(IEventAggregator eventAggregator, RTree<IEntity> tree, IEntityFactory ef)
        {
            EA = eventAggregator;
            EA.GetEvent<SelectedEntityChangedEvent>().Subscribe(SelectedItemChangedExecute);
            ITreeItemViewModelFactory TreeItemViewModelFactory = new TreeItemViewModelFactory(eventAggregator);


            EF = ef;

            entitytree = tree;

            IRTreeMember<IEntity> Head = EntityTree.AddItem(EF.CreateOrganization(), true);
            IRTreeMember<IEntity> DemoChild = EntityTree.AddItem(EF.CreateCharacter(), false);
            EntityTree.AddChild(Head, DemoChild);

            TreeHeads = new ObservableCollection<OrganizationTreeItemViewModel>();

            TreeHeads.Add(TreeItemViewModelFactory.CreateOrganizationViewModel(Head));
            RaisePropertyChanged(nameof(TreeHeads));
        }


        #region Event Handlers
        void SelectedItemChangedExecute(IEntity Selected_Item)
        {
            SelectedEntity = Selected_Item;
            //EA.GetEvent<SelectedEntityChangedEvent>().Publish(//selected entity);
        }
        #endregion

        #region Variables
        private ObservableCollection<OrganizationTreeItemViewModel> treeheads;
        public ObservableCollection<OrganizationTreeItemViewModel> TreeHeads
        {
            get { return treeheads; }
            set
            {
                SetProperty(ref treeheads, value);
            }

        }


        private RTree<IEntity> entitytree;

        public RTree<IEntity> EntityTree
        {
            get { return entitytree; }
            set { SetProperty(ref entitytree, value); }
        }



        #endregion


        #region Commands

        private DelegateCommand<OrganizationTreeItemViewModel> _commandnewcharacter;
        private DelegateCommand<OrganizationTreeItemViewModel> _commandneworganization;
        private DelegateCommand _commandcut;
        private DelegateCommand _commandcopy;
        private DelegateCommand _commandpaste;
        private DelegateCommand _commanddelete;

        public DelegateCommand<OrganizationTreeItemViewModel> CommandNewCharacter => _commandnewcharacter ??= new DelegateCommand<OrganizationTreeItemViewModel>(CommandNewCharacterExecute);
        public DelegateCommand<OrganizationTreeItemViewModel> CommandNewOrganization => _commandneworganization ??= new DelegateCommand<OrganizationTreeItemViewModel>(CommandNewOrganizationExecute);
        public DelegateCommand CommandCut => _commandcut ??= new DelegateCommand(CommandCutExecute);
        public DelegateCommand CommandCopy => _commandcopy ??= new DelegateCommand(CommandCopyExecute);
        public DelegateCommand CommandPaste => _commandpaste ??= new DelegateCommand(CommandPasteExecute);
        public DelegateCommand CommandDelete => _commanddelete ??= new DelegateCommand(CommandDeleteExecute);
        #endregion

        #region Command Handlers
        private void CommandNewCharacterExecute(OrganizationTreeItemViewModel Context)
        {
            IRTreeMember<IEntity> NewCharacter = EntityTree.AddItem(EF.CreateCharacter(), false);
            EntityTree.AddChild(Context.Target, NewCharacter);
        }
        private void CommandNewOrganizationExecute(OrganizationTreeItemViewModel Context)
        {
            IRTreeMember<IEntity> NewOrganization = EntityTree.AddItem(EF.CreateOrganization(), false);
            EntityTree.AddChild(Context.Target, NewOrganization);
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
