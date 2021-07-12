using CharacterManager.Model.Services;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CharacterManager.Model.DataLoading
{
    public class NJSONDataLoader : IDataLoader
    {
        readonly ISettingsService SS;
        private string TargetDirectory = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );

        public NJSONDataLoader(ISettingsService settingsService)
        {
            SS = settingsService;
        }

        public IDataService LoadLastFile()
        {
            string path = SS.LastUsedPath;
            IDataService LoadResult = new InvalidDataService();
            //if a previous file exists
            if (!string.IsNullOrEmpty( path ))
            {
                TargetDirectory = Path.GetDirectoryName( path );

                LoadResult = LoadFile( path, true );
            }
            return LoadResult;
        }

        public IDataService LoadWithDialog()
        {
            IDataService Load_Success = new InvalidDataService();

            string filepath;

            OpenFileDialog openFileDialog = new()
            {
                Filter = "Character Manager File (*.cmv1) |*.cmv1",
                InitialDirectory = TargetDirectory
            };

            if (openFileDialog.ShowDialog() == true)
            {
                filepath = openFileDialog.FileName;
                Load_Success = LoadFile( filepath, false );
                SS.LastUsedPath = filepath;
            }

            return Load_Success;
        }

        private IDataService LoadFile(string path, bool IgnoreExceptions)
        {
            string jsonString;

            JsonSerializerSettings settings = new()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameHandling = TypeNameHandling.All
            };

            IDataService dataService;
            try
            {
                jsonString = File.ReadAllText( path );
                dataService = JsonConvert.DeserializeObject<DataService>( jsonString, settings );
                SS.LastUsedPath = path;
            }
            catch
            {
                SS.LastUsedPath = "";
                dataService = new InvalidDataService();
                if (!IgnoreExceptions)
                {
                    throw;
                }
            }

            return dataService;
        }
    }
}
