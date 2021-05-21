using CharacterManager.Events;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.Services
{
    public class SettingsService : ISettingsService
    {
        public SettingsService(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<ProgramIsClosingEvent>().Subscribe(ProgramIsClosingEventExecute);
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
        public int OverviewColumnWidth
        {
            get
            {
                return Properties.Settings.Default.OverviewColumnWidth;
            }
            set
            {
                if (Properties.Settings.Default.OverviewColumnWidth != value)
                {
                    Properties.Settings.Default.OverviewColumnWidth = value;
                }
            }
        }
        #endregion

        #region Convenience Operators
        public string Filename
        {
            get
            {
                if(LastUsedPath.Length>3)
                {
                    string path = Path.GetFileName(LastUsedPath);
                    return path.Substring(0, path.Length - 3);
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
