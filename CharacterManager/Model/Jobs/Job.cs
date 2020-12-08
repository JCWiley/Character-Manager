using CharacterManager.Model.Events;
using CharacterManager.Model.Items;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CharacterManager.Model.Jobs
{
    public class Job : BindableBase, IJob
    {
        public Job()
        {
            job_id = Guid.NewGuid();
            Events = new List<IEvent>();
            Required_Items = new ItemCollection();
        }

        #region Derived Variables
        public int DaysRemaining
        {
            get
            {
                int val = this.duration - this.progress;
                if (val > 0)
                {
                    return val;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int EndDate
        {
            get
            {
                return this.StartDate + Duration;
            }
        }
        #endregion

        #region Variables
        private ItemCollection required_items;
        public ItemCollection Required_Items
        {
            get
            {
                return this.required_items;
            }
            set
            {
                if (this.required_items != value)
                {
                    this.required_items = value;
                    RaisePropertyChanged("Required_Items");
                }
            }
        }

        private bool complete;
        public bool Complete
        {
            get
            {
                return this.complete;
            }
            set
            {
                if (this.complete != value)
                {
                    this.complete = value;
                    RaisePropertyChanged("Complete");
                }
            }
        }

        private string summary;
        public string Summary
        {
            get
            {
                return this.summary;
            }
            set
            {
                if (this.summary != value)
                {
                    this.summary = value;
                    RaisePropertyChanged("Summary");
                }
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    RaisePropertyChanged("Description");
                }
            }
        }

        private bool iscurrentlyprogressing;
        public bool IsCurrentlyProgressing
        {
            get
            {
                return this.iscurrentlyprogressing;
            }
            set
            {
                if (this.iscurrentlyprogressing != value)
                {
                    this.iscurrentlyprogressing = value;
                    RaisePropertyChanged("IsCurrentlyProgressing");
                }
            }
        }

        private int recurring;
        public int Recurring
        {
            get
            {
                return this.recurring;
            }
            set
            {
                if (this.recurring != value)
                {
                    this.recurring = value;
                    RaisePropertyChanged("Recurring");
                }
            }
        }

        private int duration;
        public int Duration
        {
            get
            {
                return this.duration;
            }
            set
            {
                if (this.duration != value)
                {
                    this.duration = value;
                    RaisePropertyChanged("Duration");
                }
            }
        }

        private int progress;
        public int Progress
        {
            get
            {
                return this.progress;
            }
            set
            {
                if (this.progress != value)
                {
                    this.progress = value;
                    RaisePropertyChanged("Progress");
                }
            }
        }

        private int startdate;
        public int StartDate
        {
            get
            {
                return this.startdate;
            }
            set
            {
                if (this.startdate != value)
                {
                    this.startdate = value;
                    RaisePropertyChanged("StartDate");
                }
            }
        }

        private int successchance;
        public int SuccessChance
        {
            get
            {
                return this.successchance;
            }
            set
            {
                if (this.successchance != value)
                {
                    this.successchance = value;
                    RaisePropertyChanged("SuccessChance");
                }
            }
        }

        private int failurechance;
        public int FailureChance
        {
            get
            {
                return this.failurechance;
            }
            set
            {
                if (this.failurechance != value)
                {
                    this.failurechance = value;
                    RaisePropertyChanged("FailureChance");
                }
            }
        }

        private Guid ownerentity;
        public Guid OwnerEntity
        {
            get
            {
                return this.ownerentity;
            }
            set
            {
                if (this.ownerentity != value)
                {
                    this.ownerentity = value;
                    RaisePropertyChanged("OwnerEntity");
                }
            }
        }

        private Guid ownerjob;
        public Guid OwnerJob
        {
            get
            {
                return this.ownerjob;
            }
            set
            {
                if (this.ownerjob != value)
                {
                    this.ownerjob = value;
                    RaisePropertyChanged("OwnerJob");
                }
            }
        }

        private Guid job_id;
        public Guid Job_ID
        {
            get
            {
                return this.job_id;
            }
            set
            {
                if (this.job_id != value)
                {
                    this.job_id = value;
                    RaisePropertyChanged("Job_ID");
                }
            }
        }

        private List<IEvent> events;
        public List<IEvent> Events
        {
            get
            {
                return this.events;
            }
            set
            {
                if (this.events != value)
                {
                    this.events = value;
                    RaisePropertyChanged("Events");
                }
            }
        }
        #endregion

    }
}

