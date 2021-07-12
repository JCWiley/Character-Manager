using Prism.Mvvm;

namespace CharacterManager.Model.Events
{
    public class Event : BindableBase, IEvent
    {

        #region Data_Members
        private string event_type;
        public string Event_Type
        {
            get
            {
                return event_type;
            }
            set
            {
                if (event_type != value)
                {
                    event_type = value;
                    RaisePropertyChanged( nameof( Event_Type ) );
                }
            }
        }

        private string comment;
        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    RaisePropertyChanged( nameof( Comment ) );
                }
            }
        }

        private int day;
        public int Day
        {
            get
            {
                return day;
            }
            set
            {
                if (day != value)
                {
                    day = value;
                    RaisePropertyChanged( nameof( Day ) );
                }
            }
        }

        private string character;
        public string Character
        {
            get
            {
                return character;
            }
            set
            {
                if (character != value)
                {
                    character = value;
                    RaisePropertyChanged( nameof( Character ) );
                }
            }
        }

        private string job;
        public string Job
        {
            get
            {
                return job;
            }
            set
            {
                if (job != value)
                {
                    job = value;
                    RaisePropertyChanged( nameof( Job ) );
                }
            }
        }

        private int progress_effects;
        public int Progress_Effects
        {
            get
            {
                return progress_effects;
            }
            set
            {
                if (progress_effects != value)
                {
                    progress_effects = value;
                    RaisePropertyChanged( nameof( Progress_Effects ) );
                }
            }
        }
        #endregion
    }
}
