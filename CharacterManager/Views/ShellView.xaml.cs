using CharacterManager.Views.DayViews;
using CharacterManager.Views.DetailViews;
using CharacterManager.Views.MenuViews;
using CharacterManager.Views.TreeViews;
using Prism.Events;
using Prism.Regions;
using System.Windows;

namespace CharacterManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        readonly IRegionManager RM;
        private readonly IEventAggregator EA;
        public ShellView(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            InitializeComponent();

            RM = regionManager;
            EA = eventAggregator;

            RM.RegisterViewWithRegion( "MENU_REGION", typeof( MenuView ) );
            RM.RegisterViewWithRegion( "OVERVIEW_REGION", typeof( EntityListView ) );
            RM.RegisterViewWithRegion( "DETAIL_REGION", typeof( OrganizationDetailView ) );
            RM.RegisterViewWithRegion( "DETAIL_REGION", typeof( CharacterDetailView ) );
            RM.RegisterViewWithRegion( "DAY_REGION", typeof( DayControlView ) );

        }
    }
}
