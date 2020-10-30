using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Core.ViewModels
{
    public class RootViewModel : MvxViewModel
    {

        private DayViewModel dvm;

        public DayViewModel DVM
        {
            get { return dvm; }
            set { dvm = value; }
        }


        //public EntityListViewModel ELVM;

        public RootViewModel()
        {

        }
        public override void Prepare()
        {
            //base.Prepare();
        }

        public override Task Initialize()
        {
            //async setup
            DVM = Mvx.IoCProvider.IoCConstruct<DayViewModel>();
            //ELVM = Mvx.IoCProvider.IoCConstruct<EntityListViewModel>();
            //Mvx.

            return base.Initialize();
        }

    }
}
