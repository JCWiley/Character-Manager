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
using CharacterManager.Events.EventContainers;

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
            EA.GetEvent<NewEntityRequestEvent>().Subscribe(NewEntityRequestEventExecute);

            ITreeItemViewModelFactory TreeItemViewModelFactory = new TreeItemViewModelFactory(eventAggregator);

            EF = ef;

            entitytree = tree;

            IRTreeMember<IEntity> Head = EntityTree.AddItem(EF.CreateOrganization(), true);
            //IRTreeMember<IEntity> DemoChild = EntityTree.AddItem(EF.CreateCharacter());
            //EntityTree.AddChild(Head, DemoChild);

            TreeHeads = new ObservableCollection<OrganizationTreeItemViewModel>();

            TreeHeads.Add(TreeItemViewModelFactory.CreateOrganizationViewModel(Head));
            RaisePropertyChanged(nameof(TreeHeads));
        }


        #region Event Handlers
        void SelectedItemChangedExecute(IEntity Selected_Item)
        {
            SelectedEntity = Selected_Item;
        }
        void NewEntityRequestEventExecute(NewEntityRequestContainer Paramaters)
        {
            OrganizationTreeItemViewModel Source = Paramaters.EventSource;
            string Event_Type = Paramaters.RequestType;
            IRTreeMember<IEntity> NewItem;


            if(Event_Type == "Character")
            {
                NewItem = entitytree.AddItem(EF.CreateCharacter());
            }
            else if(Event_Type == "Organization")
            {
                NewItem = entitytree.AddItem(EF.CreateOrganization());
            }
            else
            {
                throw new Exception("Event Type is not Character or Organization");
            }
            entitytree.AddChild(Source.Target, NewItem);

            Source.RebuildChildren();

            Source.IsExpanded = true;
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
    }
}
