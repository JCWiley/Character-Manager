using CharacterManager.Events;
using CharacterManager.Model.Providers;
using Microsoft.Win32;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace CharacterManager.Model.DataLoading
{
    public class DataSaver : IDataSaver
    {
        IPrimaryProvider PP;
        IEventAggregator EA;
        private string TargetDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public DataSaver(IPrimaryProvider primaryProvider, IEventAggregator eventAggregator)
        {
            Initialize(primaryProvider);
            EA = eventAggregator;
            EA.GetEvent<DataSaveRequestEvent>().Subscribe(DataSaveRequestEventExecute);

            string path = ((ISettingsProvider)PP.SP).LastUsedPath;

            if (!string.IsNullOrEmpty(path))
            {
                TargetDirectory = Path.GetDirectoryName(path);
            }
        }
        public void Initialize(IPrimaryProvider primaryProvider)
        {
            PP = primaryProvider;
        }

        private void DataSaveRequestEventExecute(SaveRequestTypes paramaters)
        {
            bool SaveSuccessFlag = false;
            switch (paramaters)
            {
                case SaveRequestTypes.Save:
                    SaveSuccessFlag = Save();
                    break;
                case SaveRequestTypes.SaveAs:
                    SaveSuccessFlag = SaveWithDialog();
                    break;
                default:
                    break;
            }
        }

        public bool Save()
        {
            string filepath = ((ISettingsProvider)PP.SP).LastUsedPath;
            if (!string.IsNullOrEmpty(filepath))
            {
                return SaveFile(filepath);
            }
            else
            {
                return SaveWithDialog();
            }
        }

        public bool SaveWithDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Character Manager File (*.cm) |*.cm",
                InitialDirectory = TargetDirectory
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filepath = saveFileDialog.FileName;
                ((ISettingsProvider)PP.SP).LastUsedPath = filepath;
                TargetDirectory = Path.GetDirectoryName(filepath);
                return SaveFile(filepath);
            }
            return false;
        }

        private bool SaveFile(string path)
        {
            bool Load_Success = false;
            //try
            //{
                //FileStream writer = new FileStream(path, FileMode.Create);

                //var serializer = new DataContractSerializer(PP.GetType());

                //serializer.WriteObject(writer, PP);
                //writer.Close();

                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    IgnoreReadOnlyProperties = true,
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };
                string jsonstring = JsonSerializer.Serialize(PP, options);
                File.WriteAllText(path, jsonstring);
            //}
            //catch (Exception E)
            //{
            //    throw E;
            //}
            return Load_Success;
        }
    }
}
