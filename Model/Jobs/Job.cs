using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Character_Manager.Model.Entities;
using Character_Manager.Model.Collection_Classes;

namespace Character_Manager.Model.Jobs
{
    [DataContract(Name = "Job", Namespace = "Character_Manager")]
    public class Job : IJob
    {
        #region Constructors
        public Job()
        {
            summary = "";
            description = "{}";
            duration = 0;
            progress = 0;
            startdate = 0;
            complete = false;
            iscurrentlyprogressing = true;
            failurechance = 20;
            successchance = 20;
            Recurring = 0;

            owner_entity = Guid.Empty;
            owner_job = Guid.Empty;

            gid = Guid.NewGuid();

            Job_Event.JobEventChanged += HandleJobEventChanged;

            required_items = new Item_Collection();
        }
        #endregion

        #region Property_Handelers
        //public static event EventHandler JobEventOccured;
        //public void NotifyJobEventOccured()
        //{
        //    JobEventOccured?.Invoke(this, new EventArgs());

        //    NotifyPropertyChanged("Events_Collection");
        //    DM.IsDirty = true;
        //}

        //public static event EventHandler FieldIsDirty;
        //public void NotifyFieldIsDirty()
        //{
        //    FieldIsDirty?.Invoke(this, new EventArgs());
        //}
        private void HandleJobEventChanged(object sender, EventArgs e)
        {
            NotifyPropertyChanged("Events_Collection");
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            DM.IsDirty = true;
        }
        #endregion

        #region Functions
        public bool AddJobEvent(string EventType, string EventNotes, int ProgressImpact)
        {
            Job_Event JE = new Job_Event(EventType, EventNotes, startdate + progress, Parent_Name, summary, ProgressImpact);
            ec.Add(JE);
            NotifyPropertyChanged("Events_Collection");

            if (ProgressImpact + progress > duration)
            {
                Progress = duration;
            }
            else
            {
                Progress += ProgressImpact;
            }
            if (this.duration - this.progress <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void MarkJobAsComplete()
        {
            if (recurring == 1)
            {
                MainWindow.Display_Message_Box($"{Parent_Name} has completed work on recurring job {summary}", "Job Done.");
                Job_Event JE = new Job_Event("Repeatable Job Completed", "Completed", startdate + days_since_creation, Parent_Name, summary, 0);
                ec.Add(JE);
                progress = 0;
                this.NotifyPropertyChanged("DaysRemaining");
            }
            else
            {
                MainWindow.Display_Message_Box($"{Parent_Name} has completed work on {summary}", "Job Done.");
                Job_Event JE = new Job_Event("Job Completed", "Completed", startdate + days_since_creation, Parent_Name, summary, 0);
                ec.Add(JE);

                complete = true;
            }
            NotifyPropertyChanged("Events_Collection");
        }

        private bool Progressing()
        {
            if (complete)
            {
                return false;
            }
            if (StartDate >= DM.CurrentDay)
            {
                return false;
            }
            if (IsCurrentlyProgressing)
            {
                return true;
            }
            return false;
        }
        public void AdvanceDay()
        {
            days_since_creation += 1;
            int RE = Random_Event();
            if (Progressing())
            {
                if (RE != 1)
                {
                    if (MainWindow.CreateJobEventWindow(Parent_Name, Summary, RE) is Job_Event_Window J)
                    {
                        AddJobEvent(J.Get_EventType(), J.Get_EventNotes(), J.Get_ProgressImpact());
                        RE = J.Get_ProgressImpact();
                    }
                }
                if (RE + progress > duration)
                {
                    Progress = duration;
                }
                else
                {
                    Progress += RE;
                }
                if (this.duration - this.progress <= 0)
                {
                    MarkJobAsComplete();
                }
            }
        }
        //can technicly produce a value between -7 and 7 and be accepted, will confine to smaller increments for now
        public void AddSubtask()
        {
            //Job J = new Job()
            //{
            //    Owner_Job = gid,
            //};
            //DM.Jobs.Add(J);
            //Member_Jobs.Refresh();
            //this.NotifyPropertyChanged("Member_Jobs");

        }
        private int Random_Event()
        {
            bool Success = RandomNumber(1, successchance + 1) == successchance;
            bool Failure = RandomNumber(1, failurechance + 1) == failurechance;

            while (Success & Failure)
            {
                Success = RandomNumber(1, successchance + 1) == successchance;
                Failure = RandomNumber(1, failurechance + 1) == failurechance;
            }
            if (Success)
            {
                return 2; //if critical success, advance 1 extra day by default
            }
            if (Failure)
            {
                return -1; //if critical failure, lose 1 day by default
            }
            return 1;// if neither, advance 1 day as normal
        }
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }
        #endregion

        #region Tree_Members
        [DataMember(Name = "dm")]
        private DataModel dm;
        public DataModel DM
        {
            get
            {
                return dm;
            }
            set
            {
                if (this.dm != value)
                {
                    this.dm = value;
                    this.NotifyPropertyChanged("DM");
                }
            }
        }

        [DataMember(Name = "gid")]
        private Guid gid;
        public Guid Gid
        {
            get
            {
                return gid;
            }
            set
            {
                if (this.gid != value)
                {
                    this.gid = value;
                    this.NotifyPropertyChanged("Gid");
                }
            }
        }

        [DataMember(Name = "owner_entity")]
        private Guid owner_entity;
        public Guid Owner_Entity
        {
            get
            {
                return owner_entity;
            }
            set
            {
                if (this.owner_entity != value)
                {
                    this.owner_entity = value;
                    this.NotifyPropertyChanged("Owner_Entity_Object");
                }
            }
        }

        [DataMember(Name = "owner_job")]
        private Guid owner_job;
        public Guid Owner_Job
        {
            get
            {
                return owner_job;
            }
            set
            {
                if (this.owner_job != value)
                {
                    this.owner_job = value;
                    this.NotifyPropertyChanged("Owner_Job_Object");
                }
            }
        }
        #endregion

        #region Utility_Members
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        private int days_since_creation = 0;
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
        public string Parent_Name
        {
            get
            {
                return Owner_Entity_Object.Name;
            }
        }
        public Entity Owner_Entity_Object
        {
            get
            {
                Entity Temp = DM.Entities.Values.FirstOrDefault(x => x.Gid == Owner_Entity);
                if(Temp == null)
                {
                    return new Character(Guid.Empty, DM)
                    {
                        Name = "Default Entity"
                    };
                }

                return Temp;
            }
            set
            {
                if (Owner_Entity != value.Gid)
                {
                    Owner_Entity = value.Gid;
                }
            }
        }
        public Job Owner_Job_Object
        {
            get
            {
                Job Temp = DM.Jobs.FirstOrDefault(x => x.Gid == Owner_Job);
                if (Temp == null)
                {
                    return new Job(DM)
                    {
                        Summary = "Default Job"
                    };
                }

                return Temp;
            }
            set
            {
                if (this.Owner_Job != value.Gid)
                {
                    this.Owner_Job = value.Gid;
                }
            }
        }
        public ListCollectionView Member_Jobs
        {
            get
            {
                ListCollectionView VS = new ListCollectionView(DM.Jobs)
                {
                    Filter = f => (f as Job).Owner_Job == gid
                };
                return VS;
            }
        }
        #endregion

        #region Data_Members
        [DataMember(Name = "required_items")]
        private Item_Collection required_items;
        public Item_Collection Required_Items
        {
            get
            {
                return required_items;
            }
            set
            {
                if (this.required_items != value)
                {
                    this.required_items = value;
                    this.NotifyPropertyChanged("Required_Items");
                }
            }
        }

        [DataMember(Name = "ec")]
        private Events_Collection ec = new Events_Collection();
        public Events_Collection EC
        {
            get
            {
                return ec;
            }
            set
            {
                if (this.ec != value)
                {
                    this.ec = value;
                    this.NotifyPropertyChanged("EC");
                }
            }
        }

        [DataMember(Name = "complete")]
        private bool complete;
        public bool Complete
        {
            get
            {
                return complete;
            }
            set
            {
                if (this.complete != value)
                {
                    this.complete = value;
                    this.NotifyPropertyChanged("Parent_Name");
                }
            }
        }

        [DataMember(Name = "summary")]
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
                    this.NotifyPropertyChanged("Summary");
                }
            }
        }

        [DataMember(Name = "description")]
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
                    this.NotifyPropertyChanged("Description");
                }
            }
        }

        [DataMember(Name = "iscurrentlyprogressing")]
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
                    this.NotifyPropertyChanged("IsCurrentlyProgressing");
                }
            }
        }

        [DataMember(Name = "recurring")]
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
                    this.NotifyPropertyChanged("Recurring");
                }
            }
        }

        [DataMember(Name = "duration")]
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
                    this.NotifyPropertyChanged("Duration");
                    this.NotifyPropertyChanged("DaysRemaining");
                    this.NotifyPropertyChanged("EndDate");
                }
            }
        }

        [DataMember(Name = "progress")]
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
                    this.NotifyPropertyChanged("Progress");
                    this.NotifyPropertyChanged("DaysRemaining");
                }
            }
        }

        [DataMember(Name = "startdate")]
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
                    this.NotifyPropertyChanged("StartDate");
                    this.NotifyPropertyChanged("EndDate");
                }
            }
        }

        [DataMember(Name = "successchance")]
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
                    this.NotifyPropertyChanged("SuccessChance");
                }
            }
        }

        [DataMember(Name = "failurechance")]
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
                    this.NotifyPropertyChanged("FailureChance");
                }
            }
        }

        #endregion
    }
}
