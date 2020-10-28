using CharacterManager.Core.Interfaces;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Core.ViewModels
{
    class EntityListViewModel : MvxViewModel
    {
        #region Constructors
        public EntityListViewModel(IEntityListModel I_Entity_List_Model)
        {
            entitylist = I_Entity_List_Model;
        }
        #endregion

        #region Properties
        private IEntityListModel entitylist;

        //public IEntityListModel EntityList
        //{
        //    get { return entitylist; }
        //    set
        //    {
        //        SetProperty(ref entitylist, value);
        //        RaisePropertyChanged(() => Day);
        //    }
        //}
        #endregion

        #region Functions
        #endregion

    }
}
