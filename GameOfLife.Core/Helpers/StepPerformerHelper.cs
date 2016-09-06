using System.Collections;

namespace GameOfLife.Core.Helpers
{
    internal class StepPerformerHelper
    {
        private SpaceGrid                   _spaceGrid;
        private BitArray[]                  _stepBuffer;
        private AliveNeighborsCounterHelper _neighCounter;

        public SpaceGrid TheSpaceGrid
        {
            get
            {
                return _spaceGrid;
            }
        }

        private BitArray[] createStepBuffer(int dimension)
        {
            var buffer = new BitArray[dimension];

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = new BitArray(dimension);
            }

            return buffer;
        }
        private void clearStepBuffer()
        {
            for (int i = 0; i < _stepBuffer.Length; i++)
            {
                _stepBuffer[i].Xor(_stepBuffer[i]);
            }
        }

        public StepPerformerHelper(SpaceGrid spaceGrid)
        {
            _spaceGrid = spaceGrid;
            _stepBuffer = createStepBuffer(TheSpaceGrid.Dimension);
            _neighCounter = new AliveNeighborsCounterHelper(TheSpaceGrid);

            _spaceGrid.SpaceGridScaled  += (sender, e) => { 
                _stepBuffer = createStepBuffer(e.NewDimension); 
            };
            _spaceGrid.SpaceGridReset   += (sender, e) => {
                clearStepBuffer();
            };
        }

        private void performOnUpperLeftCell()
        {
            int i = 0;
            int j = 0;

            int aliveNeighborsCount = _neighCounter.CountForUpperLeftCell();
            applyStepRules(i, j, aliveNeighborsCount);
        }
        private void performOnUpperRightCell()
        {
            int i = 0;
            int j = TheSpaceGrid.Dimension - 1;

            int aliveNeighborsCount = _neighCounter.CountForUpperRightCell();
            applyStepRules(i, j, aliveNeighborsCount);
        }
        private void performOnLowerLeftCell()
        {
            int i = TheSpaceGrid.Dimension - 1;
            int j = 0;

            int aliveNeighborsCount = _neighCounter.CountForLowerLeftCell();
            applyStepRules(i, j, aliveNeighborsCount);
        }
        private void performOnLowerRightCell()
        {
            int i = TheSpaceGrid.Dimension - 1;
            int j = TheSpaceGrid.Dimension - 1;

            int aliveNeighborsCount = _neighCounter.CountForLowerRightCell();
            applyStepRules(i, j, aliveNeighborsCount);
        }

        private void performOnUpperBound()
        {
            int i = 0;
            int exclusiveBound = TheSpaceGrid.Dimension - 1;

            for (int j = 1; j < exclusiveBound; j++)
            {
                int aliveNeighborsCount = _neighCounter.CountForUpperCell(j);
                applyStepRules(i, j, aliveNeighborsCount);
            }
        }
        private void performOnLowerBound()
        {
            int i = TheSpaceGrid.Dimension - 1;
            int exclusiveBound = TheSpaceGrid.Dimension - 1;

            for (int j = 1; j < exclusiveBound; j++)
            {
                int aliveNeighborsCount = _neighCounter.CountForLowerCell(j);
                applyStepRules(i, j, aliveNeighborsCount);
            }
        }
        private void performOnLeftBound()
        {
            int j = 0;
            int exclusiveBound = TheSpaceGrid.Dimension - 1;

            for (int i = 1; i < exclusiveBound; i++)
            {
                int aliveNeighborsCount = _neighCounter.CountForLeftCell(i);
                applyStepRules(i, j, aliveNeighborsCount);
            }
        }
        private void performOnRightBound()
        {
            int j = TheSpaceGrid.Dimension - 1;
            int exclusiveBound = TheSpaceGrid.Dimension - 1;

            for (int i = 1; i < exclusiveBound; i++)
            {
                int aliveNeighborsCount = _neighCounter.CountForRightCell(i);
                applyStepRules(i, j, aliveNeighborsCount);
            }
        }

        private void performOnInternity()
        {
            int exclusiveBound = TheSpaceGrid.Dimension - 1;

            for (int i = 1; i < exclusiveBound; i++)
            {
                for (int j = 1; j < exclusiveBound; j++)
                {
                    int aliveNeighborsCount = _neighCounter.CountForInternalCell(i, j);
                    applyStepRules(i, j, aliveNeighborsCount);
                }
            }
        }

        private void applyStepRules(int i, int j, int aliveNeighborsCount)
        {
            if (TheSpaceGrid.IsCellAlive(i, j))
            {
                if (aliveNeighborsCount < GameConsts.UNDER_POPUL_UB_EX ||
                    aliveNeighborsCount > GameConsts.OVER_POPUL_LB_EX)
                {
                    _stepBuffer[i][j] = false;
                }
                else
                {
                    _stepBuffer[i][j] = true;
                }
            }
            else
            {
                if (aliveNeighborsCount == GameConsts.REPRODUCTION_MARK)
                {
                    _stepBuffer[i][j] = true;
                }
            }
        }

        public BitArray[] GetNewGeneration()
        {
            performOnUpperLeftCell();
            performOnUpperRightCell();
            performOnLowerLeftCell();
            performOnLowerRightCell();

            performOnUpperBound();
            performOnLowerBound();
            performOnLeftBound();
            performOnRightBound();

            performOnInternity();

            return _stepBuffer;
        }
    }
}
