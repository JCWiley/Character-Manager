﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Character_Manager
{
    [DataContract(Name = "Job_Event", Namespace = "Character_Manager")]
    public class Job_Event
    {
        #region Constructors
        public Job_Event(string I_Event_Type, string I_Comment, int I_Day, string I_Character, string I_Job, int I_Progress_Effects)
        {
            event_type = I_Event_Type;
            comment = I_Comment;
            day = I_Day;
            character = I_Character;
            job = I_Job;
            progress_effects = I_Progress_Effects;
        }
        #endregion

        #region Property_Handelers
        public static event EventHandler JobEventChanged;
        public void NotifyJobEventChanged()
        {
            JobEventChanged?.Invoke(this, new EventArgs());

            NotifyFieldIsDirty();
        }

        public static event EventHandler FieldIsDirty;
        public void NotifyFieldIsDirty()
        {
            FieldIsDirty?.Invoke(this, new EventArgs());
        }
        #endregion

        #region Functions

        #endregion

        #region Tree_Members

        #endregion

        #region Utility_Members

        #endregion

        #region Data_Members
        [DataMember(Name = "event_type")]
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

        [DataMember(Name = "comment")]
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

        [DataMember(Name = "day")]
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

        [DataMember(Name = "character")]
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

        [DataMember(Name = "job")]
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

        [DataMember(Name = "progress_effects")]
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
        #endregion
    }
}
