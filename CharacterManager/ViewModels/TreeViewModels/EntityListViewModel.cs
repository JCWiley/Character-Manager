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

namespace CharacterManager.ViewModels.TreeViewModels
{
    public class EntityListViewModel : BindableBase
    {
        private IEventAggregator EA;
        
        private EntityFactory EF;
        IEntity SelectedEntity;

        public EntityListViewModel(IEventAggregator eventAggregator, RTree<IEntity> tree, EntityFactory ef)
        {
            EA = eventAggregator;
            EA.GetEvent<SelectedEntityChangedEvent>().Subscribe(SelectedItemChangedExecute);

            EF = ef;

            entitytree = tree;

            EntityTree.AddItem(EF.CreateOrganization(), true);

            TreeHead = new OrganizationTreeItemViewModel((IRTreeMember<Organization>)EntityTree.Heads[0],EntityTree);

        }

        #region Event Handlers
        void SelectedItemChangedExecute(IEntity Selected_Item)
        {
            SelectedEntity = Selected_Item;
            //EA.GetEvent<SelectedEntityChangedEvent>().Publish(//selected entity);
        }
        #endregion

        #region Variables
        private OrganizationTreeItemViewModel treehead;
        public OrganizationTreeItemViewModel TreeHead
        {
            get { return treehead; }
            set
            {
                SetProperty(ref treehead, value);
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

        private DelegateCommand _commandnewcharacter;
        private DelegateCommand _commandneworganization;
        private DelegateCommand _commandcut;
        private DelegateCommand _commandcopy;
        private DelegateCommand _commandpaste;
        private DelegateCommand _commanddelete;

        public DelegateCommand CommandNewCharacter => _commandnewcharacter ?? (_commandnewcharacter = new DelegateCommand(CommandNewCharacterExecute));
        public DelegateCommand CommandNewOrganization => _commandneworganization ?? (_commandneworganization = new DelegateCommand(CommandNewOrganizationExecute));
        public DelegateCommand CommandCut => _commandcut ?? (_commandcut = new DelegateCommand(CommandCutExecute));
        public DelegateCommand CommandCopy => _commandcopy ?? (_commandcopy = new DelegateCommand(CommandCopyExecute));
        public DelegateCommand CommandPaste => _commandpaste ?? (_commandpaste = new DelegateCommand(CommandPasteExecute));
        public DelegateCommand CommandDelete => _commanddelete ?? (_commanddelete = new DelegateCommand(CommandDeleteExecute));
        #endregion

        #region Command Handlers
        private void CommandNewCharacterExecute()
        {
        }
        private void CommandNewOrganizationExecute()
        {
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
