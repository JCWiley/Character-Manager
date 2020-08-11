using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Character_Manager
{
    /// <summary>
    /// https://www.codeproject.com/Articles/55168/Drag-and-Drop-Feature-in-WPF-TreeView-Control
    /// 
    /// https://www.tutorialsteacher.com/ioc/dependency-injection
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private File_Manager  FM = new File_Manager();
        private DataModel dm;// = new DataModel();
        private List<TreeViewItem> activetree;
        private Organization activeparent;
        private Entity activeitem;
        private Guid entitybuffer;

        //private ListCollectionView lcvCharacters;

        //************************Initializers**********************//
        public MainWindow()
        {
            InitializeComponent();

            UserPreferences UP = new UserPreferences();

            this.Height = UP.WindowHeight;
            this.Width = UP.WindowWidth;
            this.Top = UP.WindowTop;
            this.Left = UP.WindowLeft;
            this.WindowState = UP.WindowState;

            this.Grid0.Width = UP.Column0;

            //lcvCharacters = CollectionViewSource.GetDefaultView(DM.Entities) as ListCollectionView;
            //lcvCharacters.Filter = cvsCharacters_Filter;

            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(TreeView));
            if (dpd != null)
            {
                dpd.AddValueChanged(Character_Tree, Character_List_Datasource_Changed);
            }

            // sender is the TreeView itself. Just iterate through the items
            // and retrieve the first one where IsMouseOver returns true.
            Character_Tree.ContextMenuOpening += (sender, e) =>
            {
                //activeitem = ((TreeView)sender).Items.OfType<TreeViewItem>().FirstOrDefault(item => item.IsMouseOver);
                activetree = null;
                activetree = (FindTreeViewItems((TreeView)sender));
                activetree.Reverse();
                
                if(activetree.Count > 0)
                {
                    activeitem = (Entity)(activetree[0].DataContext);

                    foreach(TreeViewItem item in activetree)
                    {
                        if (item.DataContext is Organization O)
                        {
                            if (O.ChildGuids.Contains(activeitem.Gid))
                            {
                                activeparent = O;
                                break;
                            }
                        }

                    }
                }
            };

            Character_Tree.ContextMenuClosing += (o, e) =>
            {
                activetree = null;
                activeparent = null;
                activeitem = null;
            };

            Change_View_Source(FM.Get_Last_File());

            this.DataContext = DM;
            DM.IsDirty = false;

            
        }

        //*************************Utilities************************//
        private void Change_View_Source(DataModel I_Collection)
        {
            if (DM is DataModel)
            {
                DM.SetEqual(I_Collection);
            }
            else
            {
                DM = I_Collection;
                this.DataContext = DM;
            }

            this.Title = FM.Filepath;

            return;
        }
        //https://stackoverflow.com/questions/29005119/get-the-parent-node-of-a-child-in-wpf-c-sharp-treeview
        //https://stackoverflow.com/questions/13379058/how-to-get-all-the-elements-of-a-wpf-treeview-as-a-list
        public List<TreeViewItem> FindTreeViewItems(Visual item)
        {
            if (item == null)
                return null;

            var result = new List<TreeViewItem>();

            if (item is FrameworkElement frameworkElement)
            {
                frameworkElement.ApplyTemplate();
            }

            Visual child = null;
            for (int i = 0, count = VisualTreeHelper.GetChildrenCount(item); i < count; i++)
            {
                child = VisualTreeHelper.GetChild(item, i) as Visual;

                if (child is TreeViewItem treeViewItem)
                {
                    Point RelativeCoords = Mouse.GetPosition(treeViewItem);
                    if (RelativeCoords.X >= 0 & RelativeCoords.Y >= 0 & RelativeCoords.X <= treeViewItem.ActualWidth & RelativeCoords.Y <= treeViewItem.ActualHeight)
                    {
                        result.Add(treeViewItem);
                    }
                }
                foreach (var childTreeViewItem in FindTreeViewItems(child))
                {
                    Point RelativeCoords = Mouse.GetPosition(childTreeViewItem);
                    if (RelativeCoords.X >= 0 & RelativeCoords.Y >= 0 & RelativeCoords.X <= childTreeViewItem.ActualWidth & RelativeCoords.Y <= childTreeViewItem.ActualHeight)
                    {
                        result.Add(childTreeViewItem);
                    }
                }
            }
            return result;
        }

        public void ClearSelection()
        {
            if(Character_Tree.SelectedItem is Entity E)
            {
                E.IsSelected = false;
            }
        }
        public void ClearFilter()
        {
            FilterSelectionComboBox.SelectedIndex = -1;
            FilterContentTextBox.Text = "";
            DM.FilterTree("Clear", "");
        }
        //*************************Handlers*************************//
        // Window Handlers
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (DM.IsDirty)
            {
                MessageBoxResult result = MessageBox.Show("Would you like to save your changes?", "Unsaved Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        FM.Save(DM);
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        //abort window close
                        e.Cancel = true;
                        break;
                }

            }
            ClearFilter();
            FM.Set_Last_File();

            UserPreferences UP = new UserPreferences
            {
                WindowHeight = this.Height,
                WindowWidth = this.Width,
                WindowTop = this.Top,
                WindowLeft = this.Left,
                WindowState = this.WindowState,
                Column0 = this.Grid0.Width
            };

            UP.Save();

        }
        static public Job_Event_Window CreateJobEventWindow(string ParentName, string Summary,int RE)
        {
            Job_Event_Window J = new Job_Event_Window(ParentName, Summary, RE);
            if (J.ShowDialog() == true)
            {
                return J;
            }
            return null;
        }
        static public void Display_Message_Box(string Text,string Header)
        {
            MessageBox.Show(Text, Header);
        }


        // Menu Items
        private void Save_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DM.IsDirty = false;
            FM.Save(DM);
        }
        private void Save_As_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DM.IsDirty = false;
            FM.Save_As(DM);
        }
        private void Open_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Tuple<bool, DataModel> Local_Return = FM.Load_With_Dialog();
            if (Local_Return.Item1 == true)
            {
                Change_View_Source(Local_Return.Item2);
            }
            else
            {
                //we didnt load a file, display a window to user maybe
            }
        }
        private void New_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            bool abort_flag = false;
            if (DM.IsDirty)
            {
                MessageBoxResult result = MessageBox.Show("Would you like to save your changes?", "Unsaved Changes", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        FM.Save(DM);
                        Change_View_Source(FM.New_Initialized());
                        break;
                    case MessageBoxResult.No:
                        Change_View_Source(FM.New_Initialized());
                        break;
                    case MessageBoxResult.Cancel:
                        //abort new document
                        abort_flag = true;
                        break;
                }
            }
            if (!abort_flag)
            {
                Change_View_Source(FM.New_Initialized());
                DM.IsDirty = false;
            }
        }
        private void Exit_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Generate_Event_Report_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            EventReportWindow ERW = new EventReportWindow(DM.Events_Summary);
            ERW.Show();
        }
        private void Generate_Job_Report_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            JobSummaryReportWindow JRW = new JobSummaryReportWindow(DM);
            JRW.Show();
        }
        //Button Handlers
        private void AdvanceDayButton_Click(object sender, RoutedEventArgs e)
        {
            Advance_Day_Window AD = new Advance_Day_Window();
            if (AD.ShowDialog() == true)
            {
                for (int i = 0; i < AD.Result; i++)
                {
                    DM.AdvanceDay(AD.Result);
                }
            }
        }
        private void Add_Character_Button_Click(object sender, RoutedEventArgs e)
        {
            Button B = sender as Button;
            Organization O = B.DataContext as Organization;
            this.ClearSelection();
            O.Add_Character();
            O.IsExpanded = true;
        }
        private void Add_Organization_Button_Click(object sender, RoutedEventArgs e)
        {
            Button B = sender as Button;
            Organization O = B.DataContext as Organization;
            this.ClearSelection();
            O.Add_Organization();
            O.IsExpanded = true;
        }
        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {
             ClearFilter();
        }
        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            if(FilterSelectionComboBox.SelectedItem is ComboBoxItem C)
            {
                DM.FilterTree(C.Content.ToString(), FilterContentTextBox.Text);
            }
        }
        //Selection Changed handlers
        private void Character_Tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (Character_Tree.SelectedItem is Character)
            {
                //if valid character is selected, enable Character Display control
                CD.Enable();
                OD.Disable();
                CD.Visibility = Visibility.Visible;
                OD.Visibility = Visibility.Hidden;
            }
            else if(Character_Tree.SelectedItem is Organization)
            {
                //if valid organization is selected, enable the organization control
                OD.Enable();
                CD.Disable();
                OD.Visibility = Visibility.Visible;
                CD.Visibility = Visibility.Hidden;
            }
            else
            {
                //if nothing is selected, display the last used item, disabled
                CD.Disable();
                OD.Disable();
            }
        }
        private void Character_List_Datasource_Changed(object sender, EventArgs e)
        {
            if(Character_Tree.SelectedItem is Entity E)
            {
                E.IsSelected = false;
            }
        }
        //Focus Changed Handlers
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

        //Width Management
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateGridSplitterWidths();
        }
        private void MainGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateGridSplitterWidths();
        }
        private void GridSplitter_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            UpdateGridSplitterWidths();
        }
        private void UpdateGridSplitterWidths()
        {
            Grid0.MaxWidth = Math.Min(MainGrid.ActualWidth, MainGrid.MaxWidth) - (Grid1.MinWidth); //+total margin
        }

        //Accessors
        public DataModel DM
        {
            get
            {
                return dm;
            }
            set
            {
                if (this.dm != value)
                {
                    this.dm = value;
                }
            }
        }

        //StackPanel context menu handelers

        private void TreeElementStackPanelContextMenu_Add_Character(object sender, RoutedEventArgs e)
        {
            if(((MenuItem)sender).DataContext is Organization O)
            {
                O.Add_Character();
            }
            else
            {
                throw new System.ArgumentException("Attempted to add character to non organization via context menu", "original");
            }
        }
        private void TreeElementStackPanelContextMenu_Add_Organization(object sender, RoutedEventArgs e)
        {
            if (((MenuItem)sender).DataContext is Organization O)
            {
                O.Add_Organization();
            }
            else
            {
                throw new System.ArgumentException("Attempted to add organization to non organization viw context menu", "original");
            }
        }
        private void TreeElementStackPanelContextMenu_Cut(object sender, RoutedEventArgs e)
        {
            if(activeparent != null & activeitem != null)
            {
                entitybuffer = activeitem.Gid;
                activeparent.Remove_Child(entitybuffer);
            }
        }
        private void TreeElementStackPanelContextMenu_Copy(object sender, RoutedEventArgs e)
        {
            if(activeitem != null)
            {
                entitybuffer = activeitem.Gid;
            }
        }
        private void TreeElementStackPanelContextMenu_Paste(object sender, RoutedEventArgs e)
        {
            if(activeitem is Organization item)
            {
                if(entitybuffer != null)
                {
                    item.Add_Existing_Entity(entitybuffer);
                }
            }
        }
        private void TreeElementStackPanelContextMenu_Delete(object sender, RoutedEventArgs e)
        {
            if (activeparent != null & activeitem != null)
            {
                activeparent.Remove_Child(activeitem.Gid);
            }
        }

        //Handelers that need sorting

    }
}
