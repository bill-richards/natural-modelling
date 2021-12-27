using System.Windows;
using evolution.presentation;
using evolution.ui.Views;
using life.ui.services;
using presentation.services;
using Prism.Ioc;
using Prism.Modularity;

namespace evolution.ui
{
    public partial class App
    {
        protected override Window CreateShell() => Container.Resolve<MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ISimulationService, SimulationService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<PresentationModule>();
        }
    }
}