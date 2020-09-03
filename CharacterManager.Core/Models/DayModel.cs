using CharacterManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Core.Models
{
    public class DayModel : IDayModel
    {
        public DayModel()
        {
            day = 0;
        }

        private int day;
        public int Day
        {
            get
            {
                return day;
            }
            set
            {
                day = value;
            }
        }

        public void IncrementDay()
        {
            Day = Day + 1;
        }
    }
}
