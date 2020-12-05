namespace CharacterManager.Model.Items
{
    public interface IItem
    {
        bool Aquired { get; set; }
        string Description { get; set; }
        string Location { get; set; }
        string Name { get; set; }
        float Weight { get; set; }
    }
}