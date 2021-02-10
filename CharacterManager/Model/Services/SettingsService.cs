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
        public int DetailColumnWidth
        {
            get
            {
                return Properties.Settings.Default.DetailColumnWidth;
            }
            set
            {
                if (Properties.Settings.Default.DetailColumnWidth != value)
                {
                    Properties.Settings.Default.DetailColumnWidth = value;
                }
            }
        }
        #endregion

        #region Convenience Operators
        public string Filename
        {
            get
            {
                return Path.GetFileName(LastUsedPath);
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
