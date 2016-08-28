namespace GameOfLife.Core.Helpers
{
    internal class AliveNeighborsCounterHelper
    {
        private SpaceGrid _spaceGrid;

        public SpaceGrid TheSpaceGrid
        {
            get
            {
                return _spaceGrid;
            }
        }

        public AliveNeighborsCounterHelper(SpaceGrid spaceGrid)
        {
            _spaceGrid = spaceGrid;
        }

        public int CountForInternalCell(int i, int j)
        {
            // In clockwise direction.
            return (TheSpaceGrid [i-1, j  ]  ? 1 : 0) +
                   (TheSpaceGrid [i-1, j+1]  ? 1 : 0) +
                   (TheSpaceGrid [i  , j+1]  ? 1 : 0) +
                   (TheSpaceGrid [i+1, j+1]  ? 1 : 0) +
                   (TheSpaceGrid [i+1, j  ]  ? 1 : 0) +
                   (TheSpaceGrid [i+1, j-1]  ? 1 : 0) +
                   (TheSpaceGrid [i  , j-1]  ? 1 : 0) +
                   (TheSpaceGrid [i-1, j-1]  ? 1 : 0);
        }

        public int CountForUpperCell(int j)
        {
            return (TheSpaceGrid [0, j-1]  ? 1 : 0) +
                   (TheSpaceGrid [0, j+1]  ? 1 : 0) +
                   (TheSpaceGrid [1, j-1]  ? 1 : 0) +
                   (TheSpaceGrid [1, j  ]  ? 1 : 0) +
                   (TheSpaceGrid [1, j+1]  ? 1 : 0);
        }
        public int CountForLowerCell(int j)
        {
            int i = TheSpaceGrid.Dimension - 1;

            return (TheSpaceGrid [i  , j-1] ? 1 : 0) +
                   (TheSpaceGrid [i  , j+1] ? 1 : 0) +
                   (TheSpaceGrid [i-1, j-1] ? 1 : 0) +
                   (TheSpaceGrid [i-1, j  ] ? 1 : 0) +
                   (TheSpaceGrid [i-1, j+1] ? 1 : 0);
        }
        public int CountForLeftCell(int i)
        {
            return (TheSpaceGrid [i-1, 0] ? 1 : 0) +
                   (TheSpaceGrid [i+1, 0] ? 1 : 0) +
                   (TheSpaceGrid [i-1, 1] ? 1 : 0) +
                   (TheSpaceGrid [i  , 1] ? 1 : 0) +
                   (TheSpaceGrid [i+1, 1] ? 1 : 0);
        }
        public int CountForRightCell(int i)
        {
            int j = TheSpaceGrid.Dimension - 1;

            return (TheSpaceGrid [i-1, j  ] ? 1 : 0) +
                   (TheSpaceGrid [i+1, j  ] ? 1 : 0) +
                   (TheSpaceGrid [i-1, j-1] ? 1 : 0) +
                   (TheSpaceGrid [i  , j-1] ? 1 : 0) +
                   (TheSpaceGrid [i+1, j-1] ? 1 : 0);
        }

        public int CountForUpperLeftCell()
        {
            return (TheSpaceGrid [0, 1] ? 1 : 0) +
                   (TheSpaceGrid [1, 0] ? 1 : 0) +
                   (TheSpaceGrid [1, 1] ? 1 : 0);
        }
        public int CountForUpperRightCell()
        {
            int j = TheSpaceGrid.Dimension - 1;

            return (TheSpaceGrid[0, j-1] ? 1 : 0) +
                   (TheSpaceGrid[1, j-1] ? 1 : 0) +
                   (TheSpaceGrid[1, j  ] ? 1 : 0);
        }
        public int CountForLowerLeftCell()
        {
            int i = TheSpaceGrid.Dimension - 1;

            return (TheSpaceGrid[i-1, 0] ? 1 : 0) +
                   (TheSpaceGrid[i-1, 1] ? 1 : 0) +
                   (TheSpaceGrid[i  , 1] ? 1 : 0);
        }
        public int CountForLowerRightCell()
        {
            int i = TheSpaceGrid.Dimension - 1;
            int j = TheSpaceGrid.Dimension - 1;

            return (TheSpaceGrid[i  , j-1] ? 1 : 0) +
                   (TheSpaceGrid[i-1, j-1] ? 1 : 0) +
                   (TheSpaceGrid[i-1, j  ] ? 1 : 0);
        }
    }
}
