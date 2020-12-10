using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Model.Providers
{
    public interface IPrimaryProvider
    {
        public void Initialize(IDerivedDataProvider derivedDataProvider, IJobDirectoryProvider jobDirectoryProvider, ISettingsProvider settingsProvider);

        public IDerivedDataProvider DDP { get; set; }
        public IJobDirectoryProvider JDP { get; set; }
        public ISettingsProvider SP { get; set; }
    }
}
