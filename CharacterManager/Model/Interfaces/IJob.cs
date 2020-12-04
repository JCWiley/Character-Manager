using CharacterManager.Model.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CharacterManager.Model.Interfaces
{
    public interface IJob
    {
        #region Data_Members
        public ObservableCollection<Item> Required_Items { get; set; }

        public bool Complete { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public bool IsCurrentlyProgressing { get; set; }

        public int Recurring { get; set; }

        public int Duration { get; set; }

        public int Progress { get; set; }

        public int StartDate { get; set; }

        public int SuccessChance { get; set; }

        public int FailureChance { get; set; }

        public Guid OwnerEntity { get; set; }

        public Guid OwnerJob { get; set; }

        public Guid Job_ID { get; set; }
        List<IEvent> Events { get; set; }
        #endregion
    }
}
