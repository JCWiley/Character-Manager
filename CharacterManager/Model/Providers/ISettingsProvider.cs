using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Model.Providers
{
    public interface ISettingsProvider
    {
        public string LastUsedPath { get; set; }
    }
}
