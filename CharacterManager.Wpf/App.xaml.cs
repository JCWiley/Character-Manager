using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.Core;

namespace CharacterManager.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MvxApplication
    {
        protected override void RegisterSetup()
        {
            this.RegisterSetupType<MvxWpfSetup<CharacterManager.Core.App>>();
        }
    }
}
