using CharacterManager.Core.Interfaces;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Core.ViewModels
{
    public class DayViewModel : MvxViewModel
    {
        private IDayModel daymodel;

        public IDayModel DayModel
        {
            get { return daymodel; }
            set { daymodel = value; }
        }


        public DayViewModel(IDayModel I_Day_Model)
        {
            DayModel = I_Day_Model;
        }

    }
}
