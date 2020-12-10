﻿
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Win32;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using System.Collections;
using System.Xml;
using CharacterManager.Model.Providers;
using Prism.Events;
using CharacterManager.Events;

namespace CharacterManager.Model.DataLoading
{
    public class DataLoader : IDataLoader
    {
        IPrimaryProvider PP;
        IEventAggregator EA;
        private string TargetDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public DataLoader(IPrimaryProvider primaryProvider, IEventAggregator eventAggregator)
        {
            Initialize(primaryProvider);
            EA = eventAggregator;
            EA.GetEvent<DataLoadRequestEvent>().Subscribe(DataLoadRequestEventExecute);
        }

        public void Initialize(IPrimaryProvider primaryProvider)
        {
            PP = primaryProvider;
        }

        private void DataLoadRequestEventExecute(string paramaters)
        {
            bool LoadSuccessFlag = false;
            switch (paramaters)
            {
                case "Last File":
                    LoadSuccessFlag = LoadLastFile();
                    break;
                case "Dialog":
                    LoadSuccessFlag = LoadWithDialog();
                    break;
                default:
                    break;
            }
            if(LoadSuccessFlag == false)
            {
                throw new Exception("File loading failed for some reason.");
            }

        }

        public bool LoadLastFile()
        {
            bool Load_Success = false;

            Load_Success = LoadFile(PP.SP.LastUsedPath());


            return Load_Success;
        }

        public bool LoadWithDialog()
        {
            bool Load_Success = false;

            string filepath = "";

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Character Manager File (*.cm) |*.cm",
                InitialDirectory = TargetDirectory
            };

            if (openFileDialog.ShowDialog() == true)
                filepath = openFileDialog.FileName;
            Load_Success = LoadFile(filepath);

            return Load_Success;
        }

        private bool LoadFile(string path)
        {
            bool Load_Success = false;
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());

                var serializer = new DataContractSerializer(PP.GetType());

                PP = (IPrimaryProvider)serializer.ReadObject(reader, true);
                reader.Close();
                fs.Close();

                Load_Success = true;
            }
            catch(Exception)
            {

            }
            return Load_Success;
        }
    }
}
