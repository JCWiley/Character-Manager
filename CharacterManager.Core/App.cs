using CharacterManager.Core.Utilities;
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
            InjectionUtilities.RegisterModels();

            Mvx.IoCProvider.RegisterSingleton<MainWindowViewModel>(Mvx.IoCProvider.IoCConstruct <MainWindowViewModel>());

            RegisterAppStart<DayViewModel>();
        }
    }
}
