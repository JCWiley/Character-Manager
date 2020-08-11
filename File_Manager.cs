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

namespace Character_Manager
{
    class File_Manager
    {
        private string filepath = "";
        private string TargetDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private DataModel LocalCollection = new DataModel();

        public DataModel Get_Last_File()
        {
            filepath = Properties.Settings.Default.LastUsedPath;
            if (!string.IsNullOrEmpty(filepath))
            {
                TargetDirectory = Path.GetDirectoryName(filepath);
                Load_File();
            }
            return LocalCollection;
        }

        public void Save(DataModel I_Object)
        {
            if (!string.IsNullOrEmpty(filepath))
            {
                Save_File(I_Object);
            }
            else
            {
                Save_As(I_Object);
            }
        }

        public void Save_As(DataModel I_Object)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Character Manager File (*.cm) |*.cm",
                InitialDirectory = TargetDirectory
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                filepath = saveFileDialog.FileName;
                TargetDirectory = Path.GetDirectoryName(filepath);
                Save_File(I_Object);
            }
        }

        private void Save_File(DataModel I_Object)
        {
            FileStream writer = new FileStream(filepath, FileMode.Create);

            var serializer = new DataContractSerializer(typeof(DataModel), null,
            0x7FFF /*maxItemsInObjectGraph*/,
            false /*ignoreExtensionDataObject*/,
            true /*preserveObjectReferences : this is where the magic happens */,
            null /*dataContractSurrogate*/);

            serializer.WriteObject(writer, I_Object);
            writer.Close();



            //XmlSerializer xs = new XmlSerializer(typeof(DataModel));
            //using (StreamWriter wr = new StreamWriter(filepath))
            //{
            //    xs.Serialize(wr, I_Object);
            //}

            //JsonSerializerOptions options = new JsonSerializerOptions
            //{
            //    WriteIndented = true,
            //};
            //string jsonstring = JsonSerializer.Serialize(I_Object, options);
            //File.WriteAllText(filepath, jsonstring);
        }

        public Tuple<bool, DataModel> Load_With_Dialog()
        {
            bool Load_Success;
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Character Manager File (*.cm) |*.cm",
                InitialDirectory = TargetDirectory
            };

            if (openFileDialog.ShowDialog() == true)
                filepath = openFileDialog.FileName;
                Load_Success = Load_File();
            
            if (Load_Success)
            {
                return new Tuple<bool, DataModel>(true, LocalCollection);
            }
            else
            {
                return new Tuple<bool, DataModel>(false, LocalCollection);
            }
                
        }

        private bool Load_File()
        {
            bool Load_Success = false;
            //XmlSerializer xs = new XmlSerializer(typeof(DataModel));
            try
            {
                FileStream fs = new FileStream(filepath, FileMode.Open);
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());

                var serializer = new DataContractSerializer(typeof(DataModel), null,
                0x7FFF /*maxItemsInObjectGraph*/,
                false /*ignoreExtensionDataObject*/,
                true /*preserveObjectReferences : this is where the magic happens */,
                null /*dataContractSurrogate*/);

                LocalCollection = (DataModel)serializer.ReadObject(reader, true);
                reader.Close();
                fs.Close();

                //using (StreamReader rd = new StreamReader(filepath))
                //{
                //    DataModel TempLocalCollection = xs.Deserialize(rd) as DataModel;
                //    LocalCollection = TempLocalCollection;
                //    Load_Success = true;
                //}

                //string jsonString = File.ReadAllText(filepath);
                //LocalCollection = JsonSerializer.Deserialize<DataModel>(jsonString);
                Load_Success = true;

            }
            catch (Exception)
            {
                Load_Success = false;
                //file could not be opened

                //Set Path as invalid
                filepath = "";

            }
            return Load_Success;
        }

        public void Set_Last_File()
        {
            Properties.Settings.Default.LastUsedPath = filepath;
            Properties.Settings.Default.Save();
        }

        public DataModel New()
        {
            filepath = "";
            LocalCollection = new DataModel();
            return LocalCollection;
        }

        public string Filepath
        {
            get
            {
                return filepath;
            }
        }

    }
}
