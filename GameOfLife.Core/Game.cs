using System.Collections;

using GameOfLife.Core.Helpers;

namespace GameOfLife.Core
{
    public class Game
    {
        private SpaceGrid                     _spaceGrid;
        private GameStatisticsHelper          _gameStatistics;
        private StepPerformerHelper           _stepPerformer;

        public int GenerationNumber
        {
            get {
                return _gameStatistics.GenerationNumber;
            }
        }

        public int AliveCellsCount
        {
            get {
                return _gameStatistics.AliveCellsCount;
            }
        }

        public Game(int spaceGridDimension)
        {
            _spaceGrid      = new SpaceGrid(spaceGridDimension);
            _gameStatistics = new GameStatisticsHelper(_spaceGrid);
            _stepPerformer  = new StepPerformerHelper(_spaceGrid);
        }

        public void ScaleSpaceGrid(int newDimension)
        {
            _spaceGrid.Scale(newDimension);
        }

        public void Reset()
        {
            _spaceGrid.Clear();
            _gameStatistics.Reset();
        }

        private void applyStepBuffer(BitArray[] stepBuffer)
        {
            for (int i = 0; i < _spaceGrid.Dimension; i++)
            {
                for (int j = 0; j < _spaceGrid.Dimension; j++)
                {
                    _spaceGrid.SetCellLivingState(i, j, stepBuffer[i][j]);
                }
            }
        }

        public void PerformStep()
        {
            applyStepBuffer(_stepPerformer.GetNewGeneration());
            _gameStatistics.Update();
        }

        public bool IsCellAlive(int i, int j)
        {
            return _spaceGrid.IsCellAlive(i, j);
        }
        public void SetCellLivingState(int i, int j, bool state)
        {
            _spaceGrid.SetCellLivingState(i, j, state);
        }
    }
}
