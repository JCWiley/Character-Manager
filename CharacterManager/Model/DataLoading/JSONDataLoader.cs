using CharacterManager.Model.Services;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text.Json;

namespace CharacterManager.Model.DataLoading
{
    public class JSONDataLoader : IDataLoader
    {
        readonly ISettingsService SS;
        private string TargetDirectory = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );

        public JSONDataLoader(ISettingsService settingsService)
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

                LoadResult = LoadFile( path );
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
                Load_Success = LoadFile( filepath );
            }

            return Load_Success;
        }

        private static IDataService LoadFile(string path)
        {
            string jsonString = File.ReadAllText( path );
            return JsonSerializer.Deserialize<DataService>( jsonString );
        }
    }
}
