namespace CharacterManager.Model.Services
{
    public interface ISettingsService
    {
        public string LastUsedPath
        {
            get; set;
        }
        public string Filename
        {
            get;
        }

        public void SaveProperties();
    }
}
