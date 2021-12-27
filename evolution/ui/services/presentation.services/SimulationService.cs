using life.ui.services;

namespace presentation.services
{
    public class SimulationService : ISimulationService
    {
        public int PopulationSize { get; set; }

        public int Generations { get; set; }

        public int MaximumCellSize { get; set; }

        public int MinimumCellSize { get; set; }

        public int GridCellSize { get; set; }
        public int CyclesPerGeneration { get; set; }
    }
}