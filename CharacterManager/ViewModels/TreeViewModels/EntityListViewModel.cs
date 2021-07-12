using CharacterManager.Events;
using CharacterManager.Events.EventContainers;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Factories;
using CharacterManager.Model.Providers;
using CharacterManager.Model.RedundantTree;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace CharacterManager.ViewModels.TreeViewModels
{
    public class EntityListViewModel : BindableBase
    {
        private readonly IEventAggregator EA;
        private readonly IEntityProvider EP;
        private readonly ITreeItemViewModelFactory TreeItemViewModelFactory;

        public EntityListViewModel(IEventAggregator eventAggregator, IEntityProvider entityProvider, TreeItemViewModelFactory treeItemViewModelFactory)
        {
            //assign event aggregator to local variable for later use
            EA = eventAggregator;
            EA.GetEvent<NewEntityRequestEvent>().Subscribe( NewEntityRequestEventExecute );
            EA.GetEvent<DataLoadSuccessEvent>().Subscribe( DataLoadSuccessEventExecute );

            EA.GetEvent<FilterRequestEvent>().Subscribe( FilterRequestEventExecute );

            EA.GetEvent<UIUpdateRequestEvent>().Subscribe( UIUpdateRequestExecute );

            TreeItemViewModelFactory = treeItemViewModelFactory;

            EP = entityProvider;

            TreeHeads = new ObservableCollection<OrganizationTreeItemViewModel>
            {

                //currently only uses the first head specified in the RTree, eventually plan to add multi head RTrees
                //RTrees currently support multi heading, TreeHeads does not
                TreeItemViewModelFactory.CreateOrganizationViewModel(null,EP.HeadEntities()[0])
            };
            RaisePropertyChanged( nameof( TreeHeads ) );
        }



        #region Variables
        private ObservableCollection<OrganizationTreeItemViewModel> treeheads;
        public ObservableCollection<OrganizationTreeItemViewModel> TreeHeads
        {
            get
            {
                return treeheads;
            }
            set
            {
                SetProperty( ref treeheads, value );
            }

        }
        #endregion

        #region Event Handlers
        void UIUpdateRequestExecute(ChangeType type)
        {
            switch (type)
            {
                case ChangeType.SelectedCharacterChanged:
                    break;
                case ChangeType.SelectedOrganizationChanged:
                    break;
                case ChangeType.EntityListChanged:
                    TreeHeads[0].RebuildChildren();
                    break;
                case ChangeType.JobEventListChanged:
                    break;
                case ChangeType.JobListChanged:
                    break;
                case ChangeType.DayAdvanced:
                    break;
                default:
                    break;
            }
        }
        void DataLoadSuccessEventExecute(IRTreeMember<IEntity> NewHead)
        {
            TreeHeads = new ObservableCollection<OrganizationTreeItemViewModel>
            {
                TreeItemViewModelFactory.CreateOrganizationViewModel(null,NewHead)
            };
        }
        void NewEntityRequestEventExecute(NewEntityRequestContainer Paramaters)
        {
            OrganizationTreeItemViewModel Source = Paramaters.EventSource;
            IRTreeMember<IEntity> NewItem;

            NewItem = EP.AddEntity( Paramaters.EntityType );

            EP.AddChild( Source.Target, NewItem );

            Source.RebuildChildren();

            Source.IsExpanded = true;

            EA.GetEvent<SelectedEntityChangedEvent>().Publish( NewItem );
        }

        private void FilterRequestEventExecute(FilterRequestEventContainer paramaters)
        {
            foreach (OrganizationTreeItemViewModel head in TreeHeads)
            {
                head.ApplyFilter( paramaters );
            }
        }
        #endregion
    }
}
