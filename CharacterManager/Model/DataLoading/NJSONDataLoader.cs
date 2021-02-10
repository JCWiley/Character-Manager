using CharacterManager.Model.Services;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.DataLoading
{
    public class NJSONDataLoader :IDataLoader
    {
        ISettingsService SS;
        private string TargetDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public NJSONDataLoader(ISettingsService settingsService)
        {
            SS = settingsService;
        }

        public IDataService LoadLastFile()
        {
            string path = SS.LastUsedPath;
            IDataService LoadResult = new InvalidDataService();
            //if a previous file exists
            if (!string.IsNullOrEmpty(path))
            {
                TargetDirectory = Path.GetDirectoryName(path);

                LoadResult = LoadFile(path,true);
            }
            return LoadResult;
        }

        public IDataService LoadWithDialog()
        {
            IDataService Load_Success = new InvalidDataService();

            string filepath = "";

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Character Manager File (*.cm) |*.cm",
                InitialDirectory = TargetDirectory
            };

            if (openFileDialog.ShowDialog() == true)
            {
                filepath = openFileDialog.FileName;
                Load_Success = LoadFile(filepath,false);
            }

            return Load_Success;
        }

        private IDataService LoadFile(string path, bool IgnoreExceptions)
        {
            string jsonString = "";

            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameHandling = TypeNameHandling.All
            };

            IDataService dataService = new InvalidDataService();
            try
            {
                jsonString = File.ReadAllText(path);
                dataService = JsonConvert.DeserializeObject<DataService>(jsonString, settings);
            }
            catch
            {
                SS.LastUsedPath = "";
                dataService = new InvalidDataService();
                if(!IgnoreExceptions)
                {
                    throw;
                }
            }

            return dataService;
        }
    }
}
