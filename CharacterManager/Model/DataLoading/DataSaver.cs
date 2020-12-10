using CharacterManager.Events;
using CharacterManager.Model.Providers;
using Microsoft.Win32;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
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


            if (!string.IsNullOrEmpty(PP.SP.LastUsedPath))
            {
                TargetDirectory = Path.GetDirectoryName(PP.SP.LastUsedPath);
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
            string filepath = PP.SP.LastUsedPath;
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
                TargetDirectory = Path.GetDirectoryName(filepath);
                return SaveFile(filepath);
            }
            return false;
        }

        private bool SaveFile(string path)
        {
            bool Load_Success = false;
            try
            {
                FileStream writer = new FileStream(path, FileMode.Create);

                var serializer = new DataContractSerializer(PP.GetType());

                serializer.WriteObject(writer, PP);
                writer.Close();
            }
            catch (Exception)
            {

            }
            return Load_Success;
        }
    }
    }
}
