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

namespace CharacterManager.Views
{
    /// <summary>
    /// Interaction logic for EntityListView.xaml
    /// </summary>
    public partial class EntityListView : UserControl
    {

        IRegionManager RM;
        public EntityListView(IRegionManager regionManager)
        {
            InitializeComponent();

            RM = regionManager;

            //RM.RegisterViewWithRegion("FILTER_REGION", typeof());
        }
    }
}
