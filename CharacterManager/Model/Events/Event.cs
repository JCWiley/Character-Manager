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
                return this.event_type;
            }
            set
            {
                if (this.event_type != value)
                {
                    this.event_type = value;
                    RaisePropertyChanged("Event_Type");
                }
            }
        }

        private string comment;
        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                if (this.comment != value)
                {
                    this.comment = value;
                    RaisePropertyChanged("Comment");
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
                if (this.day != value)
                {
                    this.day = value;
                    RaisePropertyChanged("Day");
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
                if (this.character != value)
                {
                    this.character = value;
                    RaisePropertyChanged("Character");
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
                if (this.job != value)
                {
                    this.job = value;
                    RaisePropertyChanged("Job");
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
                if (this.progress_effects != value)
                {
                    this.progress_effects = value;
                    RaisePropertyChanged("Progress_Effects");
                }
            }
        }
        #endregion
    }
}
