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
using Prism.Mvvm;
using System.Reflection;

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

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName.Replace("Views.", "ViewModels.").Replace("View.","ViewModel.");
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = $"{viewName}Model, {viewAssemblyName}";
                return Type.GetType(viewModelName);
            });
        }
    }
}
