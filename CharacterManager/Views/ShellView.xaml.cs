using Prism.Regions;
using System.Windows;
using CharacterManager.Views.TreeViews;
using CharacterManager.Views.DetailViews;
using Prism.Events;

namespace CharacterManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        IRegionManager RM;
        private IEventAggregator EA;
        public ShellView(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            InitializeComponent();

            RM = regionManager;
            EA = eventAggregator;

            //RM.RegisterViewWithRegion("MENU_REGION", typeof());
            RM.RegisterViewWithRegion("OVERVIEW_REGION", typeof(EntityListView));
            RM.RegisterViewWithRegion("DETAIL_REGION", typeof(OrganizationDetailView));
            RM.RegisterViewWithRegion("DETAIL_REGION", typeof(CharacterDetailView));
            

        }
    }
}
