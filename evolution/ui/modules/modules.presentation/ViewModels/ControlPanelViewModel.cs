using System.Windows.Input;
using evolution.ui;
using evolution.ui.events;
using life.ui.services;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace evolution.presentation.ViewModels
{
    public class ControlPanelViewModel : RegionViewModelBase
    {
        private readonly ISimulationService _simulationService;
        private int _gridCellSize;
        private int _minimumCellSize;
        private int _maximumCellSize;
        private int _populationSize;
        private int _generations;
        private bool _simulationIsRunning;
        private bool _simulationIsStopped;
        private bool _simulationIsPaused;
        private bool _canEnterNewPopulationData;
        private int _cyclesPerGeneration;

        public ControlPanelViewModel(IRegionManager regionManager, ISimulationService simulationService, IEventAggregator aggregator) : base(regionManager)
        {
            _simulationService = simulationService;

            aggregator.GetEvent<SimulationRunningEvent>().Subscribe(() =>
            {
                if (SimulationIsRunning) return;

                SimulationIsRunning = true;
                SimulationIsPaused = false;
                CanEnterNewPopulationData = false;
            });

            aggregator.GetEvent<SimulationStoppedEvent>().Subscribe(() =>
            {
                if (!SimulationIsRunning) return;

                SimulationIsRunning = false;
                SimulationIsPaused = true;
                CanEnterNewPopulationData = true;
            });

            StopCommand = new DelegateCommand(aggregator.GetEvent<StopRunningSimulationEvent>().Publish, () => SimulationIsRunning)
                .ObservesCanExecute(() => SimulationIsRunning);
            RunCommand = new DelegateCommand(aggregator.GetEvent<RunNewSimulationEvent>().Publish, () => SimulationIsStopped)
                .ObservesCanExecute(() => SimulationIsStopped);
            ResumeCommand = new DelegateCommand(aggregator.GetEvent<ResumeSimulationEvent>().Publish, () => SimulationIsPaused)
                .ObservesCanExecute(() => SimulationIsPaused);

            SimulationIsRunning = false;

            PopulationSize = 1000;
            Generations = 500;
            GridCellSize = 1;
            MaximumCellSize = 3;
            MinimumCellSize = 1;
            CyclesPerGeneration = -1;
            CanEnterNewPopulationData = true;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public ICommand StopCommand { get; }
        public ICommand RunCommand { get; }
        public ICommand ResumeCommand { get; }

        public bool CanEnterNewPopulationData
        {
            get => _canEnterNewPopulationData;
            set => SetProperty(ref _canEnterNewPopulationData, value);
        }

        public bool SimulationIsRunning
        {
            get => _simulationIsRunning;
            set
            {
                SetProperty(ref _simulationIsRunning, value);
                SimulationIsStopped = !value;
            }
        }

        public bool SimulationIsStopped
        {
            get => _simulationIsStopped;
            set => SetProperty(ref _simulationIsStopped, value);
        }
        public bool SimulationIsPaused
        {
            get => _simulationIsPaused;
            set => SetProperty(ref _simulationIsPaused, value);
        }

        public int PopulationSize
        {
            get => _populationSize;
            set
            {
                SetProperty(ref _populationSize, value);
                _simulationService.PopulationSize = value;
            }
        }

        public int Generations
        {
            get => _generations;
            set
            {
                SetProperty(ref _generations, value);
                _simulationService.Generations = value;
            }
        }

        public int CyclesPerGeneration
        {
            get => _cyclesPerGeneration;
            set
            {
                SetProperty(ref _cyclesPerGeneration, value);
                _simulationService.CyclesPerGeneration = value;
            }
        }


        public int MaximumCellSize
        {
            get => _maximumCellSize;
            set
            {
                SetProperty(ref _maximumCellSize, value);
                _simulationService.MaximumCellSize = value;
            }
        }

        public int MinimumCellSize
        {
            get => _minimumCellSize;
            set
            {
                SetProperty(ref _minimumCellSize, value);
                _simulationService.MinimumCellSize = value;
            }
        }

        public int GridCellSize
        {
            get => _gridCellSize;
            set
            {
                SetProperty(ref _gridCellSize, value);
                _simulationService.GridCellSize = value;
            }
        }
    }
}