using Prism.Events;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;

namespace CharacterManager.Views.TreeViews
{
    /// <summary>
    /// Interaction logic for EntityListView.xaml
    /// </summary>
    public partial class EntityListView : UserControl
    {
        private IEventAggregator EA;
        IRegionManager RM;
        public EntityListView(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            InitializeComponent();

            EA = eventAggregator;
            RM = regionManager;

            //RM.RegisterViewWithRegion("FILTER_REGION", typeof());
        }

        private void EnityTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //EA.GetEvent<SelectedEntityChangedEvent>().Publish(EntityTreeControl.SelectedItem as IEntity);
        }
    }
}
