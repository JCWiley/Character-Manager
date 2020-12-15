using CharacterManager.Model.Entities;
using CharacterManager.Model.Factories;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.RedundantTree;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.Services
{
    public class DataService :BindableBase, IDataService
    {
        public DataService(IRTreeFactory<IEntity> iRTreeFactory)
        {
            //try to load an existing file
            if(false)//no loading attempts yet
            { }
            else//if loading fails, initialize with default data
            {
                EntityTree = new RTree<IEntity>(iRTreeFactory);
                EntityTree.AddItem(new Organization(), true);

                Job_List = new List<IJob>();
            }

        }


        private List<IJob> job_list;
        public List<IJob> Job_List
        {
            get { return job_list; }
            set { SetProperty(ref job_list, value); }
        }

        private RTree<IEntity> entitytree;
        public RTree<IEntity> EntityTree
        {
            get { return entitytree; }
            set { SetProperty(ref entitytree, value); }
        }
    }
}
