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

namespace Character_Manager
{
    public class Job : INotifyPropertyChanged
    {
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
        
        //Notification code
        public static event EventHandler JobEventOccured;
        public void NotifyJobEventOccured()
        {
            JobEventOccured?.Invoke(this, new EventArgs());

            NotifyPropertyChanged("Events_Collection");
            NotifyFieldIsDirty();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public static event EventHandler FieldIsDirty;
        public void NotifyFieldIsDirty()
        {
            FieldIsDirty?.Invoke(this, new EventArgs());
        }
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        //Management Code
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

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
        [XmlIgnoreAttribute]
        public Entity Owner_Entity_Object
        {
            get
            {
                return DataModel.Entities.Values.FirstOrDefault(x => x.Gid == Owner_Entity);//fix
            }
            set
            {
                if(Owner_Entity != value.Gid)
                {
                    Owner_Entity = value.Gid;
                }
            }
        }
        [XmlIgnoreAttribute]
        public Job Owner_Job_Object
        {
            get
            {
                return DataModel.Jobs.FirstOrDefault(x => x.Gid == Owner_Job);
            }
            set
            {
                if (this.Owner_Job != value.Gid)
                {
                    this.Owner_Job = value.Gid;
                }
            }
        }
        [XmlIgnoreAttribute]
        public ListCollectionView Member_Jobs
        {
            get
            {
                ListCollectionView VS = new ListCollectionView(DataModel.Jobs)
                {
                    Filter = f => (f as Job).Owner_Job == gid
                };
                return VS;
            }
        }

        public void TriggerJobEvent()
        {
            int RE = 0;
            Job_Event_Window J = new Job_Event_Window(Parent_Name, summary, RE);
            if (J.ShowDialog() == true)
            {
                Job_Event JE = new Job_Event();
                JE.Populate(J.Get_EventType(), J.Get_EventNotes(), startdate + progress, Parent_Name, summary, J.Get_ProgressImpact());
                ec.Add(JE);
                this.NotifyJobEventOccured();
                RE = J.Get_ProgressImpact();
                if (RE + progress > duration)
                {
                    Progress = duration;
                }
                else
                {
                    Progress += RE;
                }
                if (this.duration - this.progress == 0)
                {
                    MessageBox.Show($"{Parent_Name} has completed work on {summary}", "Job Done.");
                    Job_Event JE2 = new Job_Event();
                    JE2.Populate("Job Completed", "Completed", startdate + progress, Parent_Name, summary, 0);
                    ec.Add(JE2);
                    this.NotifyJobEventOccured();
                    complete = true;
                }
            }
        }
        private void HandleJobEventChanged(object sender, EventArgs e)
        {
            this.NotifyJobEventOccured();
        }
        public void AdvanceDay()
        {
            days_since_creation += 1;
            int RE = Random_Event();
            if (!complete)
            {
                if (iscurrentlyprogressing & (StartDate >= DataModel.CurrentDay))
                {
                    if (RE != 1)
                    {
                        Job_Event_Window J = new Job_Event_Window(Parent_Name, summary, RE);
                        if (J.ShowDialog() == true)
                        {
                            Job_Event JE = new Job_Event();
                            JE.Populate(J.Get_EventType(), J.Get_EventNotes(), startdate + days_since_creation, Parent_Name, summary, J.Get_ProgressImpact());
                            ec.Add(JE);
                            this.NotifyJobEventOccured();
                            RE = J.Get_ProgressImpact();
                        }
                        else
                        {
                            RE = 1;
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

                }
                if (this.duration - this.progress == 0)
                {
                    if (recurring == 1)
                    {
                        MessageBox.Show($"{Parent_Name} has completed work on recurring job {summary}", "Job Done.");
                        Job_Event JE = new Job_Event();
                        JE.Populate("Repeatable Job Completed", "Completed", startdate + days_since_creation, Parent_Name, summary, 0);
                        ec.Add(JE);
                        this.NotifyJobEventOccured();
                        progress = 0;
                        this.NotifyPropertyChanged("DaysRemaining");
                    }
                    else
                    {
                        MessageBox.Show($"{Parent_Name} has completed work on {summary}", "Job Done.");
                        Job_Event JE = new Job_Event();
                        JE.Populate("Job Completed", "Completed", startdate + days_since_creation, Parent_Name, summary, 0);
                        ec.Add(JE);
                        this.NotifyJobEventOccured();
                        complete = true;
                    }
                }
            }
        }
        //can technicly produce a value between -7 and 7 and be accepted, will confine to smaller increments for now
        public void AddSubtask()
        {
            Job J = new Job()
            {
                Owner_Job = gid,
            };
            DataModel.Jobs.Add(J);
            Member_Jobs.Refresh();
            this.NotifyPropertyChanged("Member_Jobs");

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

        //Data Code
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

        [XmlIgnoreAttribute]
        public string Parent_Name
        {
            get
            {
                return Owner_Entity_Object.Name;
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
                    this.NotifyPropertyChanged("Summary");
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
                    this.NotifyPropertyChanged("Description");
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
                    this.NotifyPropertyChanged("IsCurrentlyProgressing");
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
                    this.NotifyPropertyChanged("Recurring");
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
                    this.NotifyPropertyChanged("Duration");
                    this.NotifyPropertyChanged("DaysRemaining");
                    this.NotifyPropertyChanged("EndDate");
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
                    this.NotifyPropertyChanged("Progress");
                    this.NotifyPropertyChanged("DaysRemaining");
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
                    this.NotifyPropertyChanged("StartDate");
                    this.NotifyPropertyChanged("EndDate");
                }
            }
        }

        [XmlIgnoreAttribute]
        public int EndDate
        {
            get
            {
                return this.StartDate + Duration;
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
                    this.NotifyPropertyChanged("SuccessChance");
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
                    this.NotifyPropertyChanged("FailureChance");
                }
            }
        }
        [XmlIgnoreAttribute]
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

        private int days_since_creation = 0;
    }
}
