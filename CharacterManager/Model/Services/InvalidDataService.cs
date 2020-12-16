using CharacterManager.Model.Entities;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.RedundantTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.Services
{
    public class InvalidDataService : IDataService
    {
        public List<IJob> Job_List { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public RTree<IEntity> EntityTree { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
