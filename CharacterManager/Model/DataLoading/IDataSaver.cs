namespace CharacterManager.Model.DataLoading
{
    public interface IDataSaver
    {
        public void Save(object Target);
        public void SaveWithDialog(object Target);
    }
}
