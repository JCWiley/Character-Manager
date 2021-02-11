using CharacterManager.Model.RedundantTree;
using CharacterManager.Events;
using CharacterManager.Model.Entities;
using Prism.Events;
using Prism.Mvvm;
using System;
using CharacterManager.Model.Factories;
using System.Collections.ObjectModel;
using CharacterManager.Events.EventContainers;
using CharacterManager.Model.Providers;

namespace CharacterManager.ViewModels.TreeViewModels
{
    public class EntityListViewModel : BindableBase
    {
        private IEventAggregator EA;
        private IEntityProvider EP;
        private ITreeItemViewModelFactory TreeItemViewModelFactory;

        public EntityListViewModel(IEventAggregator eventAggregator,IEntityProvider entityProvider,TreeItemViewModelFactory treeItemViewModelFactory)
        {
            //assign event aggregator to local variable for later use
            EA = eventAggregator;
            EA.GetEvent<NewEntityRequestEvent>().Subscribe(NewEntityRequestEventExecute);
            EA.GetEvent<DataLoadSuccessEvent>().Subscribe(DataLoadSuccessEventExecute);

            EA.GetEvent<FilterRequestEvent>().Subscribe(FilterRequestEventExecute);

            TreeItemViewModelFactory = treeItemViewModelFactory;

            EP = entityProvider;

            TreeHeads = new ObservableCollection<OrganizationTreeItemViewModel>();
            
            //currently only uses the first head specified in the RTree, eventually plan to add multi head RTrees
            //RTrees currently support multi heading, TreeHeads does not
            TreeHeads.Add(TreeItemViewModelFactory.CreateOrganizationViewModel(EP.HeadEntities()[0]));
            RaisePropertyChanged(nameof(TreeHeads));
        }



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
        #endregion

        #region Event Handlers
        void DataLoadSuccessEventExecute(IRTreeMember<IEntity> NewHead)
        {
            TreeHeads = new ObservableCollection<OrganizationTreeItemViewModel>();
            TreeHeads.Add(TreeItemViewModelFactory.CreateOrganizationViewModel(NewHead));
        }
        void NewEntityRequestEventExecute(NewEntityRequestContainer Paramaters)
        {
            OrganizationTreeItemViewModel Source = Paramaters.EventSource;
            IRTreeMember<IEntity> NewItem;

            NewItem = EP.AddEntity(Paramaters.EntityType);

            EP.AddChild(Source.Target, NewItem);

            Source.RebuildChildren();

            Source.IsExpanded = true;
        }

        private void FilterRequestEventExecute(FilterRequestEventContainer paramaters)
        {
            foreach (OrganizationTreeItemViewModel head in TreeHeads)
            {
                head.ApplyFilter(paramaters);
            }
        }
        #endregion
    }
}
