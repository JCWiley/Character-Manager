using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace CharacterManager.Model.Providers
{
    [DataContract(Name = "PrimaryProvider", Namespace = "CharacterManager.Model.Providers")]
    public class PrimaryProvider : IPrimaryProvider
    {
        public PrimaryProvider()
        {

        }
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

        public void SetEqual(IPrimaryProvider primaryProvider)
        {
            ((DerivedDataProvider)DDP).SetEqual(primaryProvider.DDP);
            ((JobDirectoryProvider)JDP).SetEqual(primaryProvider.JDP);
            ((SettingsProvider)SP).SetEqual(primaryProvider.SP);
        }

        [DataMember(Name = "DDP")]
        private object ddp;
        public object DDP
        {
            get { return ddp; }
            set { ddp = value; }
        }

        private object jdp;
        [DataMember(Name = "JDP")]
        public object JDP
        {
            get { return jdp; }
            set { jdp = value; }
        }

        private object sp;
        [JsonIgnore]
        public object SP
        {
            get { return sp; }
            set { sp = value; }
        }




    }
}
