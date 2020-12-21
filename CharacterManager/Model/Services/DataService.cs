﻿using CharacterManager.Events;
using CharacterManager.Model.DataLoading;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Factories;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.RedundantTree;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CharacterManager.Model.Services
{
    public class DataService :BindableBase, IDataService
    {
        #region variables
        private IDataLoader DL;
        private IDataSaver DS;
        private IEventAggregator EA;

        private List<IJob> job_list;
        //public List<object> Job_List_JSON
        //{
        //    get
        //    {
        //        return job_list.Cast<object>().ToList();
        //    }
        //    set
        //    {
        //        job_list = new List<IJob>();
        //        foreach (IJob job in value)
        //        {
        //            job_list.Add(job);
        //        }
        //    }
        //}
        //[JsonIgnore]
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

        #endregion

        public DataService()
        {

        }

        public DataService(IRTreeFactory<IEntity> iRTreeFactory,IDataLoader dataLoader, IDataSaver dataSaver, IEventAggregator eventAggregator)
        {
            DL = dataLoader;
            DS = dataSaver;

            EA = eventAggregator;

            EA.GetEvent<DataSaveRequestEvent>().Subscribe(DataSaveRequestEventExecute);
            EA.GetEvent<DataLoadRequestEvent>().Subscribe(DataLoadRequestEventExecute);

            //try to load an existing file
            if (false)//no loading attempts yet
            { }
            else//if loading fails, initialize with default data
            {
                EntityTree = new RTree<IEntity>(iRTreeFactory);
                EntityTree.AddItem(new Organization(), true);

                Job_List = new List<IJob>();
            }

        }

        private void DataSaveRequestEventExecute(SaveRequestTypes saveRequestType)
        {
            switch (saveRequestType)
            {
                case SaveRequestTypes.Save:
                    DS.Save((object)this);
                    break;
                case SaveRequestTypes.SaveAs:
                    DS.SaveWithDialog((object)this);
                    break;
                default:
                    break;
            }
        }
        private void DataLoadRequestEventExecute(LoadRequestTypes loadRequestType)
        {
            IDataService LoadResult = new InvalidDataService();
            switch (loadRequestType)
            {
                case LoadRequestTypes.LastFile:
                    LoadResult = DL.LoadLastFile();
                    break;
                case LoadRequestTypes.Dialog:
                    LoadResult = DL.LoadWithDialog();
                    break;
                default:
                    break;
            }
            if(LoadResult is not InvalidDataService)
            {
                SetEqual(LoadResult);
                EA.GetEvent<DataLoadSuccessEvent>().Publish(EntityTree.Heads[0]);
            }
            
        }

        private void SetEqual(IDataService dataService)
        {
            Job_List = dataService.Job_List;
            EntityTree = dataService.EntityTree;
        }
        

    }
}