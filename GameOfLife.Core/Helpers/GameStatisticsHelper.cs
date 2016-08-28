namespace GameOfLife.Core.Helpers
{
    public class GameStatisticsHelper
    {
        private SpaceGrid _spaceGrid;

        public SpaceGrid TheSpaceGrid
        {
            get { return _spaceGrid; }
        }

        public int GenerationNumber { get; set; }
        public int AliveCellsCount { get; set; }

        public GameStatisticsHelper(SpaceGrid spaceGrid)
        {
            _spaceGrid = spaceGrid;
        }


        public void Update()
        {
            ++GenerationNumber;
            AliveCellsCount = TheSpaceGrid.CountAliveCells();
        }

        public void Reset()
        {
            GenerationNumber = 0;
            AliveCellsCount = 0;
        }
    }
}
