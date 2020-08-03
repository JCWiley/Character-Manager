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
using System.Windows.Shapes;

namespace Character_Manager
{
    /// <summary>
    /// Interaction logic for EventReportWindow.xaml
    /// </summary>
    public partial class EventReportWindow : Window
    {
        public EventReportWindow(Events_Collection EC )
        {
            InitializeComponent();
            EventDisplayDataGrid.ItemsSource = EC;
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
