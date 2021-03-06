﻿using CharacterManager.Events;
using Prism.Events;
using System.IO;

namespace CharacterManager.Model.Services
{
    public class SettingsService : ISettingsService
    {
        public SettingsService(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<ProgramIsClosingEvent>().Subscribe( ProgramIsClosingEventExecute );
        }

        #region Settings
        public string LastUsedPath
        {
            get
            {
                return Properties.Settings.Default.LastUsedPath;
            }
            set
            {
                if (Properties.Settings.Default.LastUsedPath != value)
                {
                    Properties.Settings.Default.LastUsedPath = value;
                }

            }
        }
        #endregion

        #region Convenience Operators
        public string Filename
        {
            get
            {
                if (LastUsedPath.Length > 3)
                {
                    string path = Path.GetFileName( LastUsedPath );
                    return path.Substring( 0, path.Length - 5 );
                }
                return "";
            }
        }
        #endregion

        public void SaveProperties()
        {
            Properties.Settings.Default.Save();
        }

        #region Event Handlers
        private void ProgramIsClosingEventExecute(string paramaters)
        {
            SaveProperties();
        }
        #endregion


    }
}
