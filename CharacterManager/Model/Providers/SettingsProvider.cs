using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Model.Providers
{
    public class SettingsProvider : ISettingsProvider
    {
        public string LastUsedPath()
        {
            return Properties.Settings.Default.LastUsedPath;
        }
    }
}
