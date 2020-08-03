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

namespace Character_Manager
{
    class File_Manager
    {
        private string Filepath = "";
        private DataModel LocalCollection = new DataModel();

        public DataModel Get_Last_File()
        {
            Filepath = Properties.Settings.Default.LastUsedPath;
            if (!string.IsNullOrEmpty(Filepath))
            {
                Load_File();
            }
            return LocalCollection;
        }

        public void Save(DataModel I_Object)
        {
            if (!string.IsNullOrEmpty(Filepath))
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
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                Filepath = saveFileDialog.FileName;
                Save_File(I_Object);
            }
        }

        private void Save_File(DataModel I_Object)
        {
            XmlSerializer xs = new XmlSerializer(typeof(DataModel));
            using (StreamWriter wr = new StreamWriter(Filepath))
            {
                xs.Serialize(wr, I_Object);
            }
            //JsonSerializerOptions options = new JsonSerializerOptions
            //{
            //    WriteIndented = true,
            //};
            //string jsonstring = JsonSerializer.Serialize(I_Object, options);
            //File.WriteAllText(Filepath, jsonstring);
        }

        public Tuple<bool, DataModel> Load_With_Dialog()
        {
            bool Load_Success;
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Character Manager File (*.cm) |*.cm",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (openFileDialog.ShowDialog() == true)
                Filepath = openFileDialog.FileName;
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
            XmlSerializer xs = new XmlSerializer(typeof(DataModel));
            try
            {
                using (StreamReader rd = new StreamReader(Filepath))
                {
                    DataModel TempLocalCollection = xs.Deserialize(rd) as DataModel;
                    LocalCollection = TempLocalCollection;
                    Load_Success = true;
                }

                //string jsonString = File.ReadAllText(Filepath);
                //LocalCollection = JsonSerializer.Deserialize<DataModel>(jsonString);
                //Load_Success = true;

            }
            catch (Exception)
            {
                Load_Success = false;
                //file could not be opened

                //Set Path as invalid
                Filepath = "";

            }
            return Load_Success;
        }

        public void Set_Last_File()
        {
            Properties.Settings.Default.LastUsedPath = Filepath;
            Properties.Settings.Default.Save();
        }

        public DataModel New()
        {
            Filepath = "";
            LocalCollection = new DataModel();
            return LocalCollection;
        }
    }
}
