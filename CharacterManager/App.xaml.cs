using CharacterManager.Model.RedundantTree;
using CharacterManager.Model.Interfaces;
using CharacterManager.Model.Factories;
using CharacterManager.Views;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace CharacterManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            //https://prismlibrary.com/docs/wpf/getting-started.html#createshell

            Window Root = Container.Resolve<ShellView>();
            return Root;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //https://prismlibrary.com/docs/wpf/getting-started.html#registertypes
            //https://docs.microsoft.com/en-us/previous-versions/msp-n-p/ff660936(v=pandp.20)?redirectedfrom=MSDN
            containerRegistry.Register(typeof(IRTreeMember<>), typeof(RTreeMember<>));
            containerRegistry.RegisterSingleton(typeof(RTree<>),typeof(RTree<IEntity>));
            containerRegistry.Register(typeof(Guid));
            containerRegistry.Register(typeof(IRTreeFactory<>), typeof(RTreeFactory<>));
            //containerRegistry.Register(typeof(IDictionary<Guid, IRTreeMember<>>),typeof(Dictionary<Guid,IRTreeMember<>>));

            

        }
    }
}
