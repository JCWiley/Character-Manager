using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.RedundantTree;
using Prism.Events;
using Prism.Mvvm;

namespace CharacterManager.ViewModels.TreeViewModels
{
    public class CharacterTreeItemViewModel : BindableBase
    {
        public CharacterTreeItemViewModel(IRTreeMember<IEntity> target, IEventAggregator eventAggregator)
        {
            Target = target;

            EA = eventAggregator;

            Visible = true;
            IsSelected = false;
            IsExpanded = false;

            Char = (Character)Target.Item;
        }

        #region Variables
        private IEventAggregator EA;
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
                if (isselected == true)
                {
                    EA.GetEvent<SelectedEntityChangedEvent>().Publish(Char);
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
    }
}
