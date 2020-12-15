using CharacterManager.Model.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CharacterManager.Model.DataLoading
{
    public class JSONDataLoader : IDataLoader
    {
        ISettingsService SS;
        private string TargetDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public JSONDataLoader(ISettingsService settingsService)
        {
            SS = settingsService;
        }
        public object LoadLastFile()
        {
            object Load_Success = false;
            string path = SS.LastUsedPath;

            //if a previous file exists
            if (!string.IsNullOrEmpty(path))
            {
                Load_Success = LoadFile(path);
                //if LoadFile returned an actual result, set the path it used as the new default for the dialog
                if (Load_Success != (object)false)
                {
                    TargetDirectory = Path.GetDirectoryName(path);
                }
            }
            //if a previous file does not exist, do nothing, shouldnt break
            else
            {
                Load_Success = true;
            }

            return Load_Success;
        }

        public object LoadWithDialog()
        {
            object Load_Success = false;

            string filepath = "";

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Character Manager File (*.cm) |*.cm",
                InitialDirectory = TargetDirectory
            };

            if (openFileDialog.ShowDialog() == true)
            {
                filepath = openFileDialog.FileName;
                Load_Success = LoadFile(filepath);
            }

            return Load_Success;
        }

        private object LoadFile(string path)
        {
            //bool Load_Success = false;
            //try
            //{
            //FileStream fs = new FileStream(path, FileMode.Open);
            //XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());

            //var serializer = new DataContractSerializer(PP.GetType());

            //PP = (IPrimaryProvider)serializer.ReadObject(reader, true);
            //reader.Close();
            //fs.Close();

            string jsonString = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Object>(jsonString);

            //Load_Success = true;
            ////}
            ////catch(Exception E)
            ////{
            ////    throw E;
            ////}
            //return Load_Success;
        }
    }
}
