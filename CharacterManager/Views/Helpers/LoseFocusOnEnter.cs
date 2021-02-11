using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CharacterManager.Views.Helpers
{
    //https://wpfplayground.wordpress.com/2014/12/16/attached-properties-vs-behaviors/
    //https://stackoverflow.com/questions/8077556/static-function-as-eventhandler-in-xaml
    public static class LoseFocusOnEnter// : Behavior<TextBox>
    {

        public static bool GetCanLoseFocusOnEnter(UIElement obj)
        {
            return (bool)obj.GetValue(CanLoseFocusOnEnterProperty);
        }

        public static void SetCanLoseFocusOnEnter(UIElement obj, bool value)
        {
            obj.SetValue(CanLoseFocusOnEnterProperty, value);
        }

        // Using a DependencyProperty as the backing store for CanFocusOnLoad.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanLoseFocusOnEnterProperty = DependencyProperty.RegisterAttached(
            "CanLoseFocusOnEnter",
            typeof(bool),
            typeof(LoseFocusOnEnter),
            new PropertyMetadata(LoseFocusOnEnterChanged));

        private static void LoseFocusOnEnterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                if ((bool)e.NewValue == true)
                {
                    element.KeyDown += LoseFocusOnEnter_TextBlock_KeyDown;
                }
                else
                {
                    element.KeyDown -= LoseFocusOnEnter_TextBlock_KeyDown;
                }
            }
        }

        //protected override void OnAttached()
        //{
        //    base.OnAttached();
        //    this.AssociatedObject.KeyDown += LoseFocusOnEnter_TextBlock_KeyDown;
        //}

        private static void LoseFocusOnEnter_TextBlock_KeyDown(object sender, KeyEventArgs e)
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

        //protected override void OnDetaching()
        //{
        //    base.OnDetaching();
        //    this.AssociatedObject.KeyDown -= LoseFocusOnEnter_TextBlock_KeyDown;
        //}
    }
}
