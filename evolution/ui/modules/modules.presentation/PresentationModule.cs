// ReSharper disable ClassNeverInstantiated.Global

using evolution.models;
using evolution.presentation.ViewModels;
using evolution.presentation.Views;
using evolution.ui;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace evolution.presentation
{
    public class PresentationModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public PresentationModule(IRegionManager regionManager) => _regionManager = regionManager;

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(EnvironmentView));
            _regionManager.RequestNavigate(RegionNames.ControlPanelRegion, nameof(ControlPanelView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<EnvironmentView>();
            containerRegistry.RegisterForNavigation<ControlPanelView>();
            containerRegistry.Register<ICellFactory, Cell>();
            containerRegistry.Register<ICellViewModelFactory, CellViewModel>();
        }
    }
}