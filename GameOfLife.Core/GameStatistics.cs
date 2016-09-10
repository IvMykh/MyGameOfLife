using System;

namespace GameOfLife.Core
{
    public class GameStatistics
    {
        private SpaceGrid _spaceGrid;

        private int _generationNumber;
        private int _aliveCellsCount;

        public int GenerationNumber 
        {
            get {
                return _generationNumber;
            }
            private set {
                _generationNumber = value;
                OnGenerationNumberChanged(EventArgs.Empty);
            }
        }
        public int AliveCellsCount
        {
            get {
                return _aliveCellsCount;
            }
            private set {
                _aliveCellsCount = value;
                OnAliveCellsCountChanged(EventArgs.Empty);
            }
        }

        public event EventHandler GenerationNumberChanged;
        public event EventHandler AliveCellsCountChanged;

        protected virtual void OnGenerationNumberChanged(EventArgs e)
        {
            var handler = GenerationNumberChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnAliveCellsCountChanged(EventArgs e)
        {
            var handler = AliveCellsCountChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public GameStatistics(SpaceGrid spaceGrid)
        {
            _spaceGrid = spaceGrid;

            _spaceGrid.SpaceGridReset           += (sender, e) => reset();
            _spaceGrid.StepBufferApplied        += (sender, e) => update();
            _spaceGrid.CellStateSetManually     += (sender, e) => updateAliveCellsCount();
        }

        private void updateGenerationNumber()
        {
            ++GenerationNumber;
            OnGenerationNumberChanged(EventArgs.Empty);
        }
        private void updateAliveCellsCount()
        {
            AliveCellsCount = _spaceGrid.CountAliveCells();
            OnAliveCellsCountChanged(EventArgs.Empty);
        }

        private void update()
        {
            updateGenerationNumber();
            updateAliveCellsCount();
        }

        private void reset()
        {
            GenerationNumber = 0;
            AliveCellsCount = 0;
        }
    }
}
