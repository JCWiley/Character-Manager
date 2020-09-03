using CharacterManager.Core.ViewModels;
using MvvmCross;
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
            Mvx.IoCProvider.RegisterSingleton<CharacterManager.Core.Interfaces.IDayModel>(new CharacterManager.Core.Models.DayModel());

            RegisterAppStart<DayViewModel>();
        }
    }
}
