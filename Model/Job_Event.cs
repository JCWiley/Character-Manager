using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager
{
    public class Job_Event
    {
        public Job_Event()
        {
        }
        public void Populate(string I_Event_Type, string I_Comment, int I_Day, string I_Character, string I_Job, int I_Progress_Effects)
        {
            event_type = I_Event_Type;
            comment = I_Comment;
            day = I_Day;
            character = I_Character;
            job = I_Job;
            progress_effects = I_Progress_Effects;
        }

        public static event EventHandler JobEventChanged;
        public void NotifyJobEventChanged()
        {
            if (JobEventChanged != null)
                JobEventChanged(this, new EventArgs());

            NotifyFieldIsDirty();
        }

        public static event EventHandler FieldIsDirty;
        public void NotifyFieldIsDirty()
        {
            if (FieldIsDirty != null)
                FieldIsDirty(this, new EventArgs());
        }

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
                    NotifyJobEventChanged();
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
                    NotifyJobEventChanged();
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
                    NotifyJobEventChanged();
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
                    NotifyJobEventChanged();
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
                    NotifyJobEventChanged();
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
                    NotifyJobEventChanged();
                }
            }
        }
    }
}
