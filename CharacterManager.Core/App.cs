using CharacterManager.Core.Utilities;
using CharacterManager.Core.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
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

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            //Mvx.IoCProvider.RegisterSingleton<RootViewModel>(Mvx.IoCProvider.IoCConstruct <RootViewModel>());

            RegisterAppStart<RootViewModel>();
        }
    }
}
