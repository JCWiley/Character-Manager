using CharacterManager.Views.DetailViews.CharacterTabViews;
using Prism.Regions;
using System.Windows.Controls;

namespace CharacterManager.Views.DetailViews
{
    /// <summary>
    /// Interaction logic for CharacterDetailView.xaml
    /// </summary>
    public partial class CharacterDetailView : UserControl
    {
        readonly IRegionManager RM;
        public CharacterDetailView(IRegionManager regionManager)
        {
            InitializeComponent();

            RM = regionManager;

            RM.RegisterViewWithRegion( "CHARACTER_REGION", typeof( CharacterTabView ) );
            RM.RegisterViewWithRegion( "CHARACTER_JOB_REGION", typeof( CharacterJobsTabView ) );
            RM.RegisterViewWithRegion( "CHARACTER_JOB_HISTORY_REGION", typeof( CharacterJobHistoryTabView ) );
            RM.RegisterViewWithRegion( "CHARACTER_INVENTORY_REGION", typeof( CharacterInventoryTabView ) );
            //RM.RegisterViewWithRegion("CHARACTER_SOCIAL_REGION", typeof());


        }
    }
}
