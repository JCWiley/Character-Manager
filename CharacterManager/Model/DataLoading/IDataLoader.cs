using CharacterManager.Model.Services;

namespace CharacterManager.Model.DataLoading
{
    public interface IDataLoader
    {
        public IDataService LoadLastFile();
        public IDataService LoadWithDialog();
    }
}
