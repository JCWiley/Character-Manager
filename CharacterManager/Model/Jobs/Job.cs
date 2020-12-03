using CharacterManager.Model.Interfaces;
using CharacterManager.Model.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CharacterManager.Model.Jobs
{
    public class Job : IJob
    {
        public ObservableCollection<Item> Required_Items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Complete { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Summary { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsCurrentlyProgressing { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Recurring { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Duration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Progress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int StartDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SuccessChance { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int FailureChance { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Guid OwnerEntity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Guid OwnerJob { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
