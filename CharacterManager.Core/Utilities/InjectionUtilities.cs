using CharacterManager.Core.Interfaces;
using CharacterManager.Core.Models;
using MvvmCross;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Core.Utilities
{
    public static class InjectionUtilities
    {
        public static void RegisterModels()
        {
            Mvx.IoCProvider.RegisterSingleton<IDayModel>(new DayModel());
        }
    }
}
