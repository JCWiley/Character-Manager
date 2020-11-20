using CharacterManager.Views.DetailViews.CharacterTabViews;
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

namespace CharacterManager.Views.DetailViews
{
    /// <summary>
    /// Interaction logic for CharacterDetailView.xaml
    /// </summary>
    public partial class CharacterDetailView : UserControl
    {
        IRegionManager RM;
        public CharacterDetailView(IRegionManager regionManager)
        {
            InitializeComponent();

            RM = regionManager;
            RM.RegisterViewWithRegion("CHARACTER_REGION", typeof(CharacterTabView));
            //RM.RegisterViewWithRegion("CHARACTER_JOB_REGION", typeof());
            //RM.RegisterViewWithRegion("CHARACTER_JOB_HISTORY_REGION", typeof());
            //RM.RegisterViewWithRegion("CHARACTER_INVENTORY_REGION", typeof());
            //RM.RegisterViewWithRegion("CHARACTER_SOCIAL_REGION", typeof());
        }
    }
}
