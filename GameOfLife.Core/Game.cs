using GameOfLife.Core.Helpers;

namespace GameOfLife.Core
{
    public class Game
    {
        private SpaceGrid               _spaceGrid;
        private GameStatistics          _gameStatistics;
        private StepPerformerHelper     _stepPerformer;

        public GameStatistics GameStatistics
        {
            get {
                return _gameStatistics;
            }
        }

        public Game(int spaceGridDimension)
        {
            _spaceGrid      = new SpaceGrid(spaceGridDimension);
            _gameStatistics = new GameStatistics(_spaceGrid);
            _stepPerformer  = new StepPerformerHelper(_spaceGrid);
        }

        public void ScaleSpaceGrid(int newDimension)
        {
            _spaceGrid.Scale(newDimension);
        }

        public void Reset()
        {
            _spaceGrid.Clear();
        }

        public void PerformStep()
        {
            _spaceGrid.ApplyStepBuffer(_stepPerformer.GetNewGeneration());
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
