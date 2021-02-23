using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.RedundantTree;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.Services
{
    public interface IDataService
    {
        public List<IJob> Job_List { get; set; }
        public RTree<IEntity> EntityTree { get; set; }
        public Dictionary<Guid, List<IEvent>> JobEventDict { get; set; }

        public int CurrentDay { get; set; }
    }
}
