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
    /// Interaction logic for Advance_Day_Window.xaml
    /// </summary>
    public partial class Advance_Day_Window : Window
    {
        private int result = -1;
        public int Result { get => result; set => result = value; }

        public Advance_Day_Window()
        {
            InitializeComponent();
            DaysTextBox.Text = "1";
           
        }

        private void Advance_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                result = int.Parse(DaysTextBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid Format, numbers must be entered as digits.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Value too large.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if(result > 0)
            {
                if(result <= 100)
                {
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Can only advance a maximum of 100 days.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Number of days must be greater then zero.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            
        }
    }
}
