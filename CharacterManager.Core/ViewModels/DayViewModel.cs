using CharacterManager.Core.Interfaces;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Core.ViewModels
{
    public class DayViewModel : MvxViewModel
    {
        #region Constructors
        public DayViewModel(IDayModel I_Day_Model)
        {
            DayModel = I_Day_Model;
        }
        #endregion

        #region Properties
        private IDayModel daymodel;

        public IDayModel DayModel
        {
            get { return daymodel; }
            set { SetProperty(ref daymodel,value); }
        }

        public int Day
        {
            get
            {
                return DayModel.Day;
            }
        }
        #endregion
        #region functions
        public void IncrementDay()
        {
            DayModel.IncrementDay();
        }
        #endregion
    }
}
