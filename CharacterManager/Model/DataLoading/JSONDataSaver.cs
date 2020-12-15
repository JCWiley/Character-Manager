using CharacterManager.Model.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CharacterManager.Model.DataLoading
{
    public class JSONDataSaver : IDataSaver
    {
        ISettingsService SS;
        private string TargetDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public JSONDataSaver(ISettingsService settingsService)
        {
            SS = settingsService;

            string path = SS.LastUsedPath;

            if (!string.IsNullOrEmpty(path))
            {
                TargetDirectory = Path.GetDirectoryName(path);
            }
        }
        public void Save(object Target)
        {
            string filepath = SS.LastUsedPath;
            if (!string.IsNullOrEmpty(filepath))
            {
                SaveFile(filepath,Target);
            }
            else
            {
                SaveWithDialog(Target);
            }
        }

        public void SaveWithDialog(object Target)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Character Manager File (*.cm) |*.cm",
                InitialDirectory = TargetDirectory
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filepath = saveFileDialog.FileName;
                SS.LastUsedPath = filepath;
                TargetDirectory = Path.GetDirectoryName(filepath);
                SaveFile(filepath,Target);
            }
        }

        private void SaveFile(string path,object Target)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                IgnoreReadOnlyProperties = true,
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            string jsonstring = JsonSerializer.Serialize(Target, options);
            File.WriteAllText(path, jsonstring);
        }
    }
}
