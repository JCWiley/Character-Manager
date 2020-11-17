using CharacterManager.Model.Entities;
using CharacterManager.Model.Interfaces;
using CharacterManager.Model.RedundantTree;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CharacterManager.ViewModels.TreeViewModels
{
    public class OrganizationTreeItemViewModel : BindableBase
    {
        public OrganizationTreeItemViewModel(IRTreeMember<IEntity> target,RTree<IEntity> rTree)
        {
            RTree = rTree;
            Target = target;


            Visible = true;
            IsSelected = false;
            IsExpanded = false;

            Children = new ObservableCollection<object>();

            Org = (Organization)Target.Item;

            IRTreeMember<IEntity> Temp;

            foreach (Guid guid in Target.Children)
            {
                Temp = rTree.Get_Item(guid);
                if(Temp.Item is Organization)
                {
                    Children.Add(new OrganizationTreeItemViewModel(Temp, RTree));
                }
                else if (Temp.Item is Character)
                {
                    Children.Add(new CharacterTreeItemViewModel(Temp, RTree));
                }
                else
                {
                    throw new Exception("IEntity is not Character or Organization");
                }
            }
        }

        #region Variables
        private RTree<IEntity> RTree;
        private IRTreeMember<IEntity> Target;

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
            set
            {
                SetProperty(ref children, value);
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
