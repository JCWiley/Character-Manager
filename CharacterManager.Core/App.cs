using CharacterManager.Core.ViewModels;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Core
{
    public class App :MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<DayViewModel>();
        }
    }
}
