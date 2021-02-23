using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.Services
{
    public interface ISettingsService
    {
        public string LastUsedPath { get; set; }
        public int OverviewColumnWidth { get; set; }
        public int DetailColumnWidth { get; set; }

        public string Filename { get; }

        public void SaveProperties();
    }
}
