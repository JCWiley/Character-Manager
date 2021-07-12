using CharacterManager.Views.DetailViews.OrganizationTabViews;
using Prism.Regions;
using System.Windows.Controls;

namespace CharacterManager.Views.DetailViews
{
    /// <summary>
    /// Interaction logic for OrganizationDetailView.xaml
    /// </summary>
    public partial class OrganizationDetailView : UserControl
    {
        readonly IRegionManager RM;
        public OrganizationDetailView(IRegionManager regionManager)
        {
            InitializeComponent();

            RM = regionManager;

            RM.RegisterViewWithRegion( "ORGANIZATION_REGION", typeof( OrganizationTabView ) );
            RM.RegisterViewWithRegion( "ORGANIZATION_JOB_REGION", typeof( OrganizationJobsTabView ) );
            RM.RegisterViewWithRegion( "ORGANIZATION_JOB_HISTORY_REGION", typeof( OrganizationJobHistoryTabView ) );
            RM.RegisterViewWithRegion( "ORGANIZATION_INVENTORY_REGION", typeof( OrganizationInventoryTabView ) );
            //RM.RegisterViewWithRegion("ORGANIZATION_SOCIAL_REGION", typeof());
        }
    }
}
