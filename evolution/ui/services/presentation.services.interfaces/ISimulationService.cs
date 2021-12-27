namespace life.ui.services
{
    public interface ISimulationService
    {
        int GridCellSize { get; set; }
        int MaximumCellSize { get; set; }
        int MinimumCellSize { get; set; }

        int PopulationSize { get; set; }
        int CyclesPerGeneration { get; set; }
        int Generations { get; set; }
    }
}