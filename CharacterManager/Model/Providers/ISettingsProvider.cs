using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Model.Providers
{
    public interface ISettingsProvider
    {
        public void SetEqual(object settingsProvider);
        public string LastUsedPath { get; set; }
    }
}
