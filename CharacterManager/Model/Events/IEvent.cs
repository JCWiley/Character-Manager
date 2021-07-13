namespace CharacterManager.Model.Events
{
    public interface IEvent
    {
        string Character { get; set; }

        string Comment { get; set; }

        int Day { get; set; }

        string Event_Type { get; set; }

        string Job { get; set; }

        int Progress_Effects { get; set; }
    }
}