namespace GameOfLife.Core.Helpers
{
    public class GameStatisticsHelper
    {
        private SpaceGrid _spaceGrid;

        public SpaceGrid TheSpaceGrid
        {
            get { return _spaceGrid; }
        }

        public int GenerationNumber { get; private set; }
        public int AliveCellsCount  { get; private set; }

        public GameStatisticsHelper(SpaceGrid spaceGrid)
        {
            _spaceGrid = spaceGrid;

            _spaceGrid.SpaceGridReset       += (sender, e) => reset();
            _spaceGrid.StepBufferApplied    += (sender, e) => update();
        }

        private void update()
        {
            ++GenerationNumber;
            AliveCellsCount = TheSpaceGrid.CountAliveCells();
        }

        private void reset()
        {
            GenerationNumber = 0;
            AliveCellsCount = 0;
        }
    }
}
