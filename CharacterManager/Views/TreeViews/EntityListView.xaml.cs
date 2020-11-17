using CharacterManager.Events;
using CharacterManager.Model.Interfaces;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
