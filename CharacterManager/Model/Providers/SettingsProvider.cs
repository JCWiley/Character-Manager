using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Model.Providers
{
    public class SettingsProvider : ISettingsProvider
    {
        public string LastUsedPath
        {
            get
            {
                return Properties.Settings.Default.LastUsedPath;
            }
            set
            {
                if(Properties.Settings.Default.LastUsedPath != value)
                {
                    Properties.Settings.Default.LastUsedPath = value;
                }
                
            }
        }

        ~SettingsProvider()
        {
            Properties.Settings.Default.Save();
        }

        public void SetEqual(object settingsProvider)
        {
            
        }
    }
}
