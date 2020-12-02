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

namespace CharacterManager.Views.DetailViews.CharacterTabViews
{
    /// <summary>
    /// Interaction logic for CharacterTabView.xaml
    /// </summary>
    public partial class CharacterTabView : UserControl
    {
        public CharacterTabView()
        {
            InitializeComponent();
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
    }
}
