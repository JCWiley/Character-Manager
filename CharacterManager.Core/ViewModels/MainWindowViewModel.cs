using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Core.ViewModels
{
    class MainWindowViewModel : MvxViewModel
    {
        public DayViewModel DVM;
        public EntityListViewModel ELVM;

        public MainWindowViewModel()
        {

        }
        public override void Prepare()
        {
            base.Prepare();
        }

        public override Task Initialize()
        {
            //async setup
            DVM = Mvx.IoCProvider.IoCConstruct<DayViewModel>();
            ELVM = Mvx.IoCProvider.IoCConstruct<EntityListViewModel>();

            return base.Initialize();
        }

    }
}
