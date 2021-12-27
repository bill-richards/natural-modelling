using Prism.Events;

namespace evolution.presentation.ViewModels
{
    public interface ICellViewModelFactory
    {
        CellViewModel Create(ICell model, int cellUnitConversion, double envHeight, double envWidth, IEventAggregator aggregator, int numberOfCycles);
    }
}