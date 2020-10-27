using CharacterManager.Core.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text;

namespace CharacterManager.Core.ViewModels
{
    public class DayViewModel : MvxViewModel
    {
        #region Constructors
        public DayViewModel(IDayModel I_Day_Model)
        {
            DayModel = I_Day_Model;
            //DayModel = new CharacterManager.Core.Models.DayModel();
            IncrementDayCommand = new MvxCommand(IncrementDay);

        }
        #endregion

        #region Properties
        private IDayModel daymodel;

        public IDayModel DayModel
        {
            get { return daymodel; }
            set 
            {
                SetProperty(ref daymodel,value);
                RaisePropertyChanged(() => Day);
            }

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
        public IMvxCommand IncrementDayCommand { get; set; }
        public void IncrementDay()
        {
            DayModel.IncrementDay();
            RaisePropertyChanged(() => Day);
        }
        #endregion
    }
}
