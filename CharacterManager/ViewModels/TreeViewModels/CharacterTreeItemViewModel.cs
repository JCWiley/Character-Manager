using CharacterManager.Model.Entities;
using CharacterManager.Model.Interfaces;
using CharacterManager.Model.RedundantTree;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.ViewModels.TreeViewModels
{
    public class CharacterTreeItemViewModel : BindableBase
    {
        public CharacterTreeItemViewModel(IRTreeMember<IEntity> target, RTree<IEntity> rTree)
        {
            RTree = rTree;
            Target = target;

            Visible = true;
            IsSelected = false;
            IsExpanded = false;

            Char = (Character)Target.Item;
        }

        #region Variables
        private RTree<IEntity> RTree;
        private IRTreeMember<IEntity> Target;

        private Character character;

        public Character Char
        {
            get { return character; }
            set { SetProperty(ref character, value); }
        }
        #endregion

        #region Commands
        private DelegateCommand _commandcut;
        private DelegateCommand _commandcopy;
        private DelegateCommand _commandpaste;
        private DelegateCommand _commanddelete;

        public DelegateCommand CommandCut => _commandcut ?? (_commandcut = new DelegateCommand(CommandCutExecute));
        public DelegateCommand CommandCopy => _commandcopy ?? (_commandcopy = new DelegateCommand(CommandCopyExecute));
        public DelegateCommand CommandPaste => _commandpaste ?? (_commandpaste = new DelegateCommand(CommandPasteExecute));
        public DelegateCommand CommandDelete => _commanddelete ?? (_commanddelete = new DelegateCommand(CommandDeleteExecute));
        #endregion

        #region Command Handlers
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
    }
}
