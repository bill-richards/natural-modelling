using System;
using System.Collections.ObjectModel;
using System.Windows;
using evolution.ui;
using evolution.ui.events;
using gsdc.common;
using life.ui.services;
using Prism.Events;
using Prism.Regions;

namespace evolution.presentation.ViewModels
{
    public class EnvironmentViewModel : RegionViewModelBase
    {
        private readonly ICellFactory _cellFactory;
        private readonly ICellViewModelFactory _viewModelFactory;
        private int _populationCount;
        private int _minimumCellSize;
        private int _cellSingleUnitConverter;
        private int _maximumCellSize;
        private double _width;
        private double _height;
        private readonly ISimulationService _simulationService;
        private readonly IEventAggregator _aggregator;

        public EnvironmentViewModel(IRegionManager regionManager, IEventAggregator aggregator, ISimulationService simulationService, ICellFactory factory, ICellViewModelFactory viewModelFactory) :
            base(regionManager)
        {
            Width = 700;
            Height = 440;
            _cellFactory = factory;
            _simulationService = simulationService;
            _aggregator = aggregator;
            _viewModelFactory = viewModelFactory;

            _aggregator.GetEvent<RunNewSimulationEvent>().Subscribe(RunNewSimulation, ThreadOption.UIThread);
        }

        private void RunNewSimulation()
        {
            Population.Clear();
            _aggregator.GetEvent<CellRemovedEvent>().Publish();
            PopulationCount = _simulationService.PopulationSize;
            MinimumCellSize = _simulationService.MinimumCellSize;
            MaximumCellSize = _simulationService.MaximumCellSize;
            CellSingleUnitConverter = _simulationService.GridCellSize;
            CyclesPerGeneration = _simulationService.CyclesPerGeneration;
            Start();
        }

        private void Start()
        {
            for (var index = 1; index <= PopulationCount; index++)
            {
                // size should probably be determined by genetics but for now we'll randomize it
                var cellSize = RandomNumberGenerator.NextInt(MinimumCellSize, MaximumCellSize);
                var cell = _cellFactory.Create(cellSize, index);

                var model = _viewModelFactory.Create(cell, CellSingleUnitConverter, Height, Width, _aggregator, CyclesPerGeneration);
                ref var element = ref model.UiRepresentation;
                Population.Add(element);
                model.Start();
            }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public ObservableCollection<UIElement> Population { get; } = new ObservableCollection<UIElement>();

        private int CyclesPerGeneration { get; set; }

        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        public int PopulationCount
        {
            get => _populationCount;
            set => SetProperty(ref _populationCount, value);
        }

        public int MinimumCellSize
        {
            get => _minimumCellSize;
            set => SetProperty(ref _minimumCellSize, value);
        }

        public int MaximumCellSize
        {
            get => _maximumCellSize;
            set => SetProperty(ref _maximumCellSize, value);
        }

        public int CellSingleUnitConverter
        {
            get => _cellSingleUnitConverter;
            set => SetProperty(ref _cellSingleUnitConverter, value);
        }
    }
}
