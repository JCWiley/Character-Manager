using Character_Manager.Model.RedundantTree;
using CharacterManager.Events;
using CharacterManager.Model.Interfaces;
using CharacterManager.Model.Entities;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.ViewModels
{
    public class EntityListViewModel : BindableBase
    {
        private IEventAggregator EA;

        public EntityListViewModel(IEventAggregator eventAggregator)
        {
            EA = eventAggregator;
            EA.GetEvent<SelectedEntityChangedEvent>().Subscribe(SelectedItemChangedExecute);
        }

        #region Event Handlers
        void SelectedItemChangedExecute(IEntity Selected_Item)
        {
            //EA.GetEvent<SelectedEntityChangedEvent>().Publish(//selected entity);
        }
        #endregion
        #region Variables
        private RTree<IEntity> entitytree;

        public RTree<IEntity> EntityTree
        {
            get { return entitytree; }
            set { SetProperty(ref entitytree,value); }
        }



        #endregion
    }
}
