﻿using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CharacterManager.Views.ListViews;

namespace CharacterManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        IRegionManager RM;
        public ShellView(IRegionManager regionManager)
        {
            InitializeComponent();

            RM = regionManager;

            //RM.RegisterViewWithRegion("MENU_REGION", typeof());
            RM.RegisterViewWithRegion("OVERVIEW_REGION", typeof(EntityListView));
            //RM.RegisterViewWithRegion("DETAIL_REGION", typeof());
        }
    }
}
