using System;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using evolution.ui;
using evolution.ui.events;
using gsdc.common;
using Prism.Events;

namespace evolution.presentation.ViewModels
{
    public class CellViewModel : ViewModelBase, ICellViewModelFactory
    {
        private enum HorizontalMovement
        {
            Left,
            Right,
        }

        private enum VerticalMovement
        {
            Up,
            Down,
        }

        private static readonly object LockObject = new object();

        private readonly Timer _moveTimer;
        private readonly ICell _cellModel;
        private readonly int _numberOfCycles;
        private readonly int _cellUnitConversion;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly Action _simulationStoppedEvent;
        private readonly Action _publishSimulationRunningEvent;
        private readonly SubscriptionToken _resumeSimulationSubscriptionToken;
        private readonly SubscriptionToken _stopRunningSimulationSubscriptionToken;

        private HorizontalMovement _horizontalMovementDirection;
        private VerticalMovement _verticalMovementDirection;
        private Color _color = Colors.BlueViolet;
        private Ellipse _uiRepresentation;
        private int _cycleCounter;
        private double _opacity;
        private double _left;
        private double _top;

        public CellViewModel Create(ICell model, int cellUnitConversion, double envHeight, double envWidth, IEventAggregator aggregator, int numberOfCycles)
        {
            return new CellViewModel(model, cellUnitConversion, envHeight, envWidth, numberOfCycles, aggregator);
        }

        public CellViewModel() { }

        private CellViewModel(ICell model, int cellUnitConversion, double envHeight, double envWidth, int numberOfCycles, IEventAggregator aggregator)
        {
            _publishSimulationRunningEvent = aggregator.GetEvent<SimulationRunningEvent>().Publish;
            _simulationStoppedEvent = aggregator.GetEvent<SimulationStoppedEvent>().Publish;

            _stopRunningSimulationSubscriptionToken = aggregator.GetEvent<StopRunningSimulationEvent>().Subscribe(() =>
            {
                lock (LockObject) { IsRunning = false; }
                _simulationStoppedEvent?.Invoke();
            }, ThreadOption.BackgroundThread);

            _resumeSimulationSubscriptionToken = aggregator.GetEvent<ResumeSimulationEvent>().Subscribe(Start, ThreadOption.BackgroundThread);

            aggregator.GetEvent<CellRemovedEvent>().Subscribe(Destroy, ThreadOption.UIThread, false);

            _cellModel = model;
            _cellUnitConversion = cellUnitConversion;
            _numberOfCycles = numberOfCycles;
            _cellModel.PropertyChanged += CellModelPropertyChanged;
            _horizontalMovementDirection = (HorizontalMovement)RandomNumberGenerator.NextInt(2);
            _verticalMovementDirection = (VerticalMovement)RandomNumberGenerator.NextInt(2);

            _moveTimer = new Timer(RandomNumberGenerator.NextInt(20, 50));
            _moveTimer.Elapsed += MovementTimerTick;

            EnvironmentHeight = envHeight;
            EnvironmentWidth = envWidth;
            Left = RandomNumberGenerator.NextInt((int)envWidth - 10);
            Top = RandomNumberGenerator.NextInt((int)envHeight - 10);

            // opacity, like color, needs to be derived from genetic data
            // but for now, we'll just use a random one
            Opacity = RandomNumberGenerator.NextInt(30, 90) / 100d;
            // color needs to be derived from genetic data
            // but for now, we'll just use a random one
            Fill = Color.FromRgb((byte)RandomNumberGenerator.NextInt(200),
                (byte)RandomNumberGenerator.NextInt(180), (byte)RandomNumberGenerator.NextInt(200));

            CreateRepresentation();
        }

        public void Start()
        {
            _cycleCounter = _numberOfCycles;
            lock (LockObject)
            {
                IsRunning = true;
            }

            _moveTimer.Start();
            _publishSimulationRunningEvent?.Invoke();
        }


        public override void Destroy()
        {
            _resumeSimulationSubscriptionToken.Dispose();
            _stopRunningSimulationSubscriptionToken.Dispose();
            if (_cellModel != null) _cellModel.PropertyChanged -= CellModelPropertyChanged;
            _moveTimer.Elapsed -= MovementTimerTick;
            if (_uiRepresentation != null) BindingOperations.ClearAllBindings(_uiRepresentation);

            _uiRepresentation = null;
        }

        private void CreateRepresentation()
        {
            _uiRepresentation = new Ellipse
            {
                Width = Size * _cellUnitConversion,
                Height = Size * _cellUnitConversion,
            };

            var binding = new Binding("Size") { Source = this };
            _uiRepresentation.SetBinding(FrameworkElement.HeightProperty, binding);
            _uiRepresentation.SetBinding(FrameworkElement.WidthProperty, binding);

            binding = new Binding("Left") { Source = this };
            _uiRepresentation.SetBinding(Canvas.LeftProperty, binding);
            binding = new Binding("Top") { Source = this };
            _uiRepresentation.SetBinding(Canvas.TopProperty, binding);

            binding = new Binding("Opacity") { Source = this };
            _uiRepresentation.SetBinding(UIElement.OpacityProperty, binding);

            _uiRepresentation.Fill = new SolidColorBrush { Color = Fill };
        }

        private void CellModelPropertyChanged(object sender, PropertyChangedEventArgs e) => RaisePropertyChanged(nameof(Size));

        private void MovementTimerTick(object sender, ElapsedEventArgs args)
        {
            if (sender is null) return;
            bool isRunning;

            lock (LockObject) { isRunning = IsRunning; }

            if (!isRunning || _cycleCounter == 0) ((Timer)sender).Stop();
            if (_cycleCounter > 0) --_cycleCounter;

            // randomize opacity
            Opacity = RandomNumberGenerator.NextInt(50, 90) / 100d;
            // randomize speed 
            ((Timer)sender).Interval = RandomNumberGenerator.NextInt(30, 50);

            // move 
            MoveHorizontally();
            MoveVertically();
        }

        private void MoveHorizontally()
        {
            // Change the horizontal movement direction (up/down) if we are at an edge
            if (Left <= 1)
                _horizontalMovementDirection = HorizontalMovement.Right;
            if (Left >= EnvironmentWidth - Size)
                _horizontalMovementDirection = HorizontalMovement.Left;

            // Randomly decide if we are going to moving in this plane
            var moveHorizontal = RandomNumberGenerator.NextInt(28) % 3 != 0;
            if (!moveHorizontal) return;

            var newPosition = Left - Size;
            switch (_horizontalMovementDirection)
            {
                case HorizontalMovement.Right:
                    newPosition = Left + Size;
                    break;
                case HorizontalMovement.Left:
                    break;
                default:
                    _horizontalMovementDirection = (HorizontalMovement)RandomNumberGenerator.NextInt(1, 3);
                    return;
            }

            if (newPosition <= 1)
                newPosition = 0;
            else if (newPosition >= EnvironmentWidth - Size)
                newPosition = EnvironmentWidth - Size;

            Left = newPosition;
        }

        private void MoveVertically()
        {
            // Change the vertical movement direction (up/down) if we are at an edge
            if (Top <= 1)
                _verticalMovementDirection = VerticalMovement.Down;
            if (Top >= EnvironmentHeight - (Size + 8))
                _verticalMovementDirection = VerticalMovement.Up;

            // Randomly decide if we are moving vertically
            var moveVertical = RandomNumberGenerator.NextInt(27) % 2 == 0;

            if (moveVertical) return;
            var newPosition = Top + Size;

            switch (_verticalMovementDirection)
            {
                case VerticalMovement.Down:
                    break;
                case VerticalMovement.Up:
                    newPosition = Top - Size;
                    break;
                default:
                    _verticalMovementDirection = (VerticalMovement)RandomNumberGenerator.NextInt(1, 3);
                    return;
            }

            if (newPosition <= 1)
                newPosition = 0;
            else if (newPosition >= EnvironmentHeight - Size)
                newPosition = EnvironmentHeight - Size;

            Top = newPosition;
        }

        public ref Ellipse UiRepresentation { get => ref _uiRepresentation; }

        public int Size => _cellModel.Size * _cellUnitConversion;

        private bool IsRunning { get; set; }

        public double Left
        {
            get => _left;
            set => SetProperty(ref _left, value);
        }

        public double Top
        {
            get => _top;
            set => SetProperty(ref _top, value);
        }

        public Color Fill
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        public double Opacity
        {
            get => _opacity;
            set => SetProperty(ref _opacity, value);
        }

        public double EnvironmentWidth { set; get; }

        public double EnvironmentHeight { set; get; }
    }
}