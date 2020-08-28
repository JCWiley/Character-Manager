using Character_Manager.Model.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Character_Manager.Templates
{
    /// <summary>
    /// Interaction logic for CharacterDisplay.xaml
    /// </summary>
    public partial class CharacterDisplay : UserControl
    {
        public CharacterDisplay()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public Character C
        {
            get { return (Character)GetValue(c); }
            set { SetValue(c, value); }
        }
        public static readonly DependencyProperty c = DependencyProperty.Register("C", typeof(Character), typeof(CharacterDisplay), new PropertyMetadata(new Character(Guid.Empty,new DataModel())));

        public int Day
        {
            get { return (int)GetValue(day); }
            set { SetValue(day, value); }
        }
        public static readonly DependencyProperty day = DependencyProperty.Register("Day", typeof(int), typeof(CharacterDisplay), new PropertyMetadata(0));

        public void Enable()
        {
            SetRTFFromString(C.Description, Character_Description_RTB);
            SetRTFFromString(C.Quirks, Quirks_RTB);

            NameTextBlock.IsEnabled = true;
            BirthPlaceTextBlock.IsEnabled = true;
            AliasTextBlock.IsEnabled = true;
            CharacterLocationComboBox.IsEnabled = true;
            RaceTextBlock.IsEnabled = true;
            OccupationTextBlock.IsEnabled = true;
            Character_Description_RTB.IsEnabled = true;
            Quirks_RTB.IsEnabled = true;
            Job_List.IsEnabled = true;
            Job_History_DataGrid.IsEnabled = true;
            Inventory_DataGrid.IsEnabled = true;
        }
        public void Disable()
        {
            Character_Description_RTB.Document.Blocks.Clear();
            Quirks_RTB.Document.Blocks.Clear();

            NameTextBlock.IsEnabled = false;
            BirthPlaceTextBlock.IsEnabled = false;
            AliasTextBlock.IsEnabled = false;
            CharacterLocationComboBox.IsEnabled = false;
            RaceTextBlock.IsEnabled = false;
            OccupationTextBlock.IsEnabled = false;
            Character_Description_RTB.IsEnabled = false;
            Quirks_RTB.IsEnabled = false;
            Job_List.IsEnabled = false;
            Job_History_DataGrid.IsEnabled = false;
            Inventory_DataGrid.IsEnabled = false;
        }

        //Utilities
        private string GetRTFStringFromRTB(RichTextBox Target)
        {
            TextRange textRange = new TextRange(Target.Document.ContentStart, Target.Document.ContentEnd);
            using (MemoryStream ms = new MemoryStream())
            {
                textRange.Save(ms, DataFormats.Rtf);
                return Encoding.ASCII.GetString(ms.ToArray());
            }
        }
        private void SetRTFFromString(string SourceString, RichTextBox Target)
        {
            byte[] DescriptionbyteArray = Encoding.ASCII.GetBytes(SourceString);
            using (MemoryStream ms = new MemoryStream(DescriptionbyteArray))
            {
                TextRange textRange = new TextRange(Target.Document.ContentStart, Target.Document.ContentEnd);
                textRange.Load(ms, DataFormats.Rtf);
            }
        }

        //Focus Changed Handlers
        private void Quirks_RTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (C is Character Local_Char)
            {
                Local_Char.Quirks = GetRTFStringFromRTB(Quirks_RTB);
            }
        }
        private void Character_Description_RTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (C is Character Local_Char)
            {
                Local_Char.Description = GetRTFStringFromRTB(Character_Description_RTB);
            }
        }
        private void Job_Details_RTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Job_List.SelectedItem is Job Local_Job)
            {
                Local_Job.Description = GetRTFStringFromRTB(Job_Details_RTB);
            }
        }
        private void LoseFocusOnEnter_TextBlock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //Keyboard.ClearFocus();
                TraversalRequest TR = new TraversalRequest(FocusNavigationDirection.Next);
                if (Keyboard.FocusedElement is UIElement keyboardfocus)
                {
                    keyboardfocus.MoveFocus(TR);
                }

                if (sender is TextBox Sender)
                {
                    Sender.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
            }
        }

        //Button Handlers
        private void Add_Job_Button_Click(object sender, RoutedEventArgs e)
        {
            Job_List.SelectedItems.Clear();
            if (C is Character localChar)
            {
                localChar.AddJob();
            }
            else
            {
                MessageBox.Show("A character must be selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        private void Custom_Event_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Job_List.SelectedItem is Job Local_Job)
            {
                if (MainWindow.CreateJobEventWindow(Local_Job.Parent_Name, Local_Job.Summary,0) is Job_Event_Window J)
                {
                    if (Local_Job.AddJobEvent(J.Get_EventType(), J.Get_EventNotes(), J.Get_ProgressImpact()))
                    {
                        MessageBox.Show($"{Local_Job.Parent_Name} has completed work on {Local_Job.Summary}", "Job Done.");
                        Local_Job.MarkJobAsComplete();
                    }
                }
            }
        }

        //Selection Changed Handlers
        private void Character_Tab_Manager_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Character_Tab_Manager.SelectedIndex == 2)
            {
                //Events_Collection Temp = C.Events_Summary;
                Job_History_DataGrid.Items.Refresh();
            }
        }
        private void Job_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Job_List.SelectedItem is Job Local_Job)
            {
                SetRTFFromString(Local_Job.Description, Job_Details_RTB);

                //enable editing
                JobControlsGrid.IsEnabled = true;
                //Job_Details_RTB.IsEnabled = true;
                //ExpectedDurationTextBox.IsEnabled = true;
                //Job_Items_Datagrid.IsEnabled = true;
                //Add_Custom_Event_Button.IsEnabled = true;
                //Repeating_Combo_Box.IsEnabled = true;
                //FailureChanceTextBox.IsEnabled = true;
                //SuccessChanceTextBox.IsEnabled = true;
            }
            else
            {
                Job_Details_RTB.Document.Blocks.Clear();

                //no valid selected job, disable editing
                JobControlsGrid.IsEnabled = false;
                //Job_Details_RTB.IsEnabled = false;
                //ExpectedDurationTextBox.IsEnabled = false;
                //Job_Items_Datagrid.IsEnabled = false;
                //Add_Custom_Event_Button.IsEnabled = false;
                //Repeating_Combo_Box.IsEnabled = false;
                //FailureChanceTextBox.IsEnabled = false;
                //SuccessChanceTextBox.IsEnabled = false;
            }
        }

        //Other
        private void Job_List_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            (e.NewItem as Job).StartDate = Day;
            (e.NewItem as Job).Owner_Entity = (C as Character).Gid;
        }
    }
}
