using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CharacterManager.Model.Providers
{
    [DataContract(Name = "PrimaryProvider", Namespace = "CharacterManager.Model.Providers")]
    public class PrimaryProvider : IPrimaryProvider
    {
        public PrimaryProvider(IDerivedDataProvider derivedDataProvider, IJobDirectoryProvider jobDirectoryProvider, ISettingsProvider settingsProvider)
        {
            Initialize(derivedDataProvider, jobDirectoryProvider, settingsProvider);
        }
        public void Initialize(IDerivedDataProvider derivedDataProvider, IJobDirectoryProvider jobDirectoryProvider, ISettingsProvider settingsProvider)
        {
            DDP = derivedDataProvider;
            JDP = jobDirectoryProvider;
            SP = settingsProvider;
        }

        [DataMember(Name = "DDP")]
        private IDerivedDataProvider ddp;
        public IDerivedDataProvider DDP
        {
            get { return ddp; }
            set { ddp = value; }
        }

        private IJobDirectoryProvider jdp;
        [DataMember(Name = "JDP")]
        public IJobDirectoryProvider JDP
        {
            get { return jdp; }
            set { jdp = value; }
        }

        private ISettingsProvider sp;
        public ISettingsProvider SP
        {
            get { return sp; }
            set { sp = value; }
        }




    }
}
