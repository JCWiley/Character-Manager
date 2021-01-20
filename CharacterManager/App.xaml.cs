using CharacterManager.Model.RedundantTree;
using CharacterManager.Model.Factories;
using CharacterManager.Views;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Windows;
using Prism.Mvvm;
using System.Reflection;
using CharacterManager.Views.DetailViews;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Providers;
using System.Collections.Generic;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.Services;
using CharacterManager.Model.DataLoading;
//using CharacterManager.Model.DataLoading;

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

            containerRegistry.Register(typeof(IRTreeFactory<>), typeof(RTreeFactory<>));
            containerRegistry.Register(typeof(IEntityFactory), typeof(EntityFactory));
            containerRegistry.Register(typeof(IJobFactory), typeof(JobFactory));
            containerRegistry.Register(typeof(IJobEventFactory), typeof(JobEventFactory));

            containerRegistry.RegisterSingleton(typeof(IDataService), typeof(DataService));
            containerRegistry.RegisterSingleton(typeof(ISettingsService), typeof(SettingsService));

            containerRegistry.RegisterSingleton(typeof(IEntityProvider), typeof(EntityProvider));
            containerRegistry.RegisterSingleton(typeof(IDerivedDataProvider), typeof(DerivedDataProvider));
            containerRegistry.RegisterSingleton(typeof(IJobDirectoryProvider), typeof(JobDirectoryProvider));
            containerRegistry.RegisterSingleton(typeof(IJobEventProvider), typeof(JobEventProvider));

            containerRegistry.RegisterSingleton(typeof(IDataLoader), typeof(NJSONDataLoader));
            containerRegistry.RegisterSingleton(typeof(IDataSaver), typeof(NJSONDataSaver));

            containerRegistry.RegisterForNavigation<CharacterDetailView>();
            containerRegistry.RegisterForNavigation<OrganizationDetailView>();

            Application.Current.Resources.Add("IoC", this.Container);
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName.Replace("Views.", "ViewModels.").Replace("View.","ViewModel.");
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = $"{viewName}Model, {viewAssemblyName}";
                var type = Type.GetType(viewModelName);
                if(type == null)
                {
                    throw new Exception($"{viewModelName} Was not found");
                }
                return type;
            });
        }
    }
}
