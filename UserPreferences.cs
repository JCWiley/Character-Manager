using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Character_Manager
{
    //Credit to https://www.codeproject.com/Articles/50761/Save-and-Restore-WPF-Window-Size-Position-and-or-S
    class UserPreferences
    {
        private double windowtop;
        private double windowleft;
        private double windowheight;
        private double windowwidth;
        private System.Windows.WindowState windowstate;

        private double column0width;
        private string column0data;

        public double WindowTop { get => windowtop; set => windowtop = value; }
        public double WindowLeft { get => windowleft; set => windowleft = value; }
        public double WindowHeight { get => windowheight; set => windowheight = value; }
        public double WindowWidth { get => windowwidth; set => windowwidth = value; }
        public System.Windows.WindowState WindowState { get => windowstate; set => windowstate = value; }

        public GridLength Column0
        {
            //https://stackoverflow.com/questions/23627015/how-to-serialize-gridlength-as-a-part-of-user-settings
            // Create the "GridLength" from the separate properties
            get
            {

                return new GridLength(this.column0width);//, (GridUnitType)Enum.Parse(typeof(GridUnitType), this.column0data));
            }
            set
            {
                // store the "GridLength" properties in separate properties
                this.column0data = value.GridUnitType.ToString();
                this.column0width = value.Value;
            }
        }

        public UserPreferences()
        {
            //Load the settings
            Load();

            //Size it to fit the current screen
            SizeToFit();

            //Move the window at least partially into view
            MoveIntoView();
        }
        private void Load()
        {
            windowtop = Properties.Settings.Default.Window_Top;
            windowleft = Properties.Settings.Default.Window_Left;
            windowheight = Properties.Settings.Default.Window_Height;
            windowwidth = Properties.Settings.Default.Window_Width;
            windowstate = Properties.Settings.Default.Window_Maximized;

            column0width = Properties.Settings.Default.Column0Width;
            column0data = Properties.Settings.Default.Column0Data;
        }
        public void Save()
        {
            if (windowstate != System.Windows.WindowState.Minimized)
            {
                Properties.Settings.Default.Window_Top = windowtop;
                Properties.Settings.Default.Window_Left = windowleft;
                Properties.Settings.Default.Window_Height = windowheight;
                Properties.Settings.Default.Window_Width = windowwidth;
                Properties.Settings.Default.Window_Maximized = windowstate;

                Properties.Settings.Default.Column0Width = column0width;

                Properties.Settings.Default.Save();
            }
        }
        public void SizeToFit()
        {
            if (windowheight > System.Windows.SystemParameters.VirtualScreenHeight)
            {
                windowheight = System.Windows.SystemParameters.VirtualScreenHeight;
            }

            if (windowwidth > System.Windows.SystemParameters.VirtualScreenWidth)
            {
                windowwidth = System.Windows.SystemParameters.VirtualScreenWidth;
            }
        }
        public void MoveIntoView()
        {
            if (windowtop + windowheight / 2 >
                 System.Windows.SystemParameters.VirtualScreenHeight)
            {
                windowtop =
                  System.Windows.SystemParameters.VirtualScreenHeight -
                  windowheight;
            }

            if (windowleft + windowwidth / 2 >
                     System.Windows.SystemParameters.VirtualScreenWidth)
            {
                windowleft =
                  System.Windows.SystemParameters.VirtualScreenWidth -
                  windowwidth;
            }

            if (windowtop < 0)
            {
                windowtop = 0;
            }

            if (windowleft < 0)
            {
                windowleft = 0;
            }
        }
    }
}
