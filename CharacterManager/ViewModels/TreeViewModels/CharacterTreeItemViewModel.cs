using CharacterManager.Model.Entities;
using CharacterManager.Model.Interfaces;
using CharacterManager.Model.RedundantTree;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.ViewModels.TreeViewModels
{
    public class CharacterTreeItemViewModel : BindableBase
    {
        public CharacterTreeItemViewModel(IRTreeMember<IEntity> target, IEventAggregator eventAggregator)
        {
            Target = target;

            Visible = true;
            IsSelected = false;
            IsExpanded = false;

            Char = (Character)Target.Item;
        }

        #region Variables
        private IRTreeMember<IEntity> Target;

        private Character character;

        public Character Char
        {
            get { return character; }
            set { SetProperty(ref character, value); }
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
