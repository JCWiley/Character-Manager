using CharacterManager.Model.Items;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace CharacterManager.Model.Jobs
{
    public class Job : BindableBase, IJob
    {
        public Job(int CurrentDay)
        {
            job_id = Guid.NewGuid();
            Required_Items = new ObservableCollection<Item>();
            Days_Since_Creation = 0;
            iscurrentlyprogressing = true;
            startdate = CurrentDay;
            Creation_Date = CurrentDay;
        }
        #region Derived Variables
        public int DaysRemaining
        {
            get
            {
                int val = duration - progress;
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
                return StartDate + Duration;
            }
        }
        #endregion

        #region Variables
        private ObservableCollection<Item> required_items;
        public ObservableCollection<Item> Required_Items
        {
            get
            {
                return required_items;
            }
            set
            {
                if (required_items != value)
                {
                    required_items = value;
                    RaisePropertyChanged( nameof( Required_Items ) );
                }
            }
        }



        private bool complete;
        public bool Complete
        {
            get
            {
                return complete;
            }
            set
            {
                if (complete != value)
                {
                    complete = value;
                    RaisePropertyChanged( nameof( Complete ) );
                }
            }
        }

        private string summary;
        public string Summary
        {
            get
            {
                return summary;
            }
            set
            {
                if (summary != value)
                {
                    summary = value;
                    RaisePropertyChanged( nameof( Summary ) );
                }
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (description != value)
                {
                    description = value;
                    RaisePropertyChanged( nameof( Description ) );
                }
            }
        }

        private bool iscurrentlyprogressing;
        public bool IsCurrentlyProgressing
        {
            get
            {
                return iscurrentlyprogressing;
            }
            set
            {
                if (iscurrentlyprogressing != value)
                {
                    iscurrentlyprogressing = value;
                    RaisePropertyChanged( nameof( IsCurrentlyProgressing ) );
                }
            }
        }

        private bool recurring;
        public bool Recurring
        {
            get
            {
                return recurring;
            }
            set
            {
                if (recurring != value)
                {
                    recurring = value;
                    RaisePropertyChanged( nameof( Recurring ) );
                }
            }
        }

        private int days_since_creation;
        public int Days_Since_Creation
        {
            get
            {
                return days_since_creation;
            }
            set
            {
                if (days_since_creation != value)
                {
                    days_since_creation = value;
                    RaisePropertyChanged( nameof( Days_Since_Creation ) );
                }
            }
        }

        public int Creation_Date
        {
            get;
        }

        private int duration;
        public int Duration
        {
            get
            {
                return duration;
            }
            set
            {
                if (duration != value)
                {
                    duration = value;
                    RaisePropertyChanged( nameof( Duration ) );
                    RaisePropertyChanged( nameof( DaysRemaining ) );
                }
            }
        }

        private int progress;
        public int Progress
        {
            get
            {
                return progress;
            }
            set
            {
                if (progress != value)
                {
                    progress = value;
                    RaisePropertyChanged( nameof( Progress ) );
                }
            }
        }

        private int startdate;
        public int StartDate
        {
            get
            {
                return startdate;
            }
            set
            {
                if (startdate != value)
                {
                    startdate = value;
                    RaisePropertyChanged( nameof( StartDate ) );
                }
            }
        }

        private int successchance;
        public int SuccessChance
        {
            get
            {
                return successchance;
            }
            set
            {
                if (successchance != value)
                {
                    successchance = value;
                    RaisePropertyChanged( nameof( SuccessChance ) );
                }
            }
        }

        private int failurechance;
        public int FailureChance
        {
            get
            {
                return failurechance;
            }
            set
            {
                if (failurechance != value)
                {
                    failurechance = value;
                    RaisePropertyChanged( nameof( FailureChance ) );
                }
            }
        }

        private Guid ownerentity;
        public Guid OwnerEntity
        {
            get
            {
                return ownerentity;
            }
            set
            {
                if (ownerentity != value)
                {
                    ownerentity = value;
                    RaisePropertyChanged( nameof( OwnerEntity ) );
                }
            }
        }

        private Guid ownerjob;
        public Guid OwnerJob
        {
            get
            {
                return ownerjob;
            }
            set
            {
                if (ownerjob != value)
                {
                    ownerjob = value;
                    RaisePropertyChanged( nameof( OwnerJob ) );
                }
            }
        }

        private Guid job_id;
        public Guid Job_ID
        {
            get
            {
                return job_id;
            }
            set
            {
                if (job_id != value)
                {
                    job_id = value;
                    RaisePropertyChanged( nameof( Job_ID ) );
                }
            }
        }
        #endregion

    }
}

