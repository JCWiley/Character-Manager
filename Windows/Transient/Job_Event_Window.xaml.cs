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
    /// Interaction logic for Job_Event_Window.xaml
    /// </summary>
    public partial class Job_Event_Window : Window
    {
        public Job_Event_Window(string I_Character,string I_Job,int I_Progress_Impact)
        {
            InitializeComponent();

            Parent_Name_Label.Content = I_Character;
            Job_Summary_Label.Content = I_Job;
            Event_Type_Label.Content = EventTypeSwitch(I_Progress_Impact);
            Progress_Impact_Combobox.SelectedIndex = I_Progress_Impact + 7;

        }

        private void Event_Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Notes_Text_Box.SelectAll();
            Notes_Text_Box.Focus();
        }

        private string EventTypeSwitch(int I_Progress_Impact)
        {
            switch(I_Progress_Impact)
            {
                case -7:
                    return "Critical Failure";
                case -6:
                    return "Critical Failure";
                case -5:
                    return "Critical Failure";
                case -4:
                    return "Critical Failure";
                case -3:
                    return "Failure";
                case -2:
                    return "Failure";
                case -1:
                    return "Failure";
                case 0:
                    return "Failure";
                case 1:
                    return "Anomaly";
                case 2:
                    return "Success";
                case 3:
                    return "Success";
                case 4:
                    return "Critical Success";
                case 5:
                    return "Critical Success";
                case 6:
                    return "Critical Success";
                case 7:
                    return "Critical Success";
            }
            return "Anomaly";
        }
        private void Progress_Impact_Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Event_Type_Label.Content = EventTypeSwitch(Progress_Impact_Combobox.SelectedIndex - 7);
        }
        public string Get_EventType()
        {
            return Event_Type_Label.Content as string;
        }
        public string Get_EventNotes()
        {
            return Notes_Text_Box.Text;
        }
        public int Get_ProgressImpact()
        {
            return Progress_Impact_Combobox.SelectedIndex - 7;
        }
    }
}
