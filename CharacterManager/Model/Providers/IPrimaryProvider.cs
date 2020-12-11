using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Model.Providers
{
    public interface IPrimaryProvider
    {
        public void Initialize(IDerivedDataProvider derivedDataProvider, IJobDirectoryProvider jobDirectoryProvider, ISettingsProvider settingsProvider);

        public void SetEqual(IPrimaryProvider primaryProvider);

        //public IDerivedDataProvider DDP { get; set; }
        //public IJobDirectoryProvider JDP { get; set; }
        //public ISettingsProvider SP { get; set; }

        public object DDP { get; set; }
        public object JDP { get; set; }
        public object SP { get; set; }
    }
}
