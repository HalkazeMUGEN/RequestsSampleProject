using Prism.Ioc;
using RequestsSampleProject.data.Script;
using RequestsSampleProject.ui.main;
using System.Windows;

namespace RequestsSampleProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IScriptRepository, ScriptRepository>();
        }
    }
}
