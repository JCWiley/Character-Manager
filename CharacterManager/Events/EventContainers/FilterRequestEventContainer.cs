namespace CharacterManager.Events.EventContainers
{
    public struct FilterRequestEventContainer
    {
        public FilterRequestEventContainer(FilterType filterType, string filtercontent)
        {
            FilterType = filterType;
            FilterContent = filtercontent;
        }

        public FilterType FilterType
        {
            get; set;
        }
        public string FilterContent
        {
            get; set;
        }
    }
}
