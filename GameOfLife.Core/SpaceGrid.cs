using System;
using System.Collections;
using System.Text;

namespace GameOfLife.Core
{
    public class SpaceGridScaledEventArgs
        : EventArgs
    {
        public int NewDimension { get; set; }
    }

    public class SpaceGrid
    {
        private BitArray[] _spaceGrid;

        public int Dimension
        {
            get {
                return _spaceGrid.Length;
            }
        }

        public bool this[int i, int j]
        {
            get {
                return _spaceGrid[i][j];
            }
        }

        private BitArray[] createSpaceGrid(int dimension)
        {
            var newSpaceGrid = new BitArray[dimension];

            for (int i = 0; i < newSpaceGrid.Length; i++)
            {
                newSpaceGrid[i] = new BitArray(dimension);
            }

            return newSpaceGrid;
        }

        public SpaceGrid(int dimension)
        {
            _spaceGrid = createSpaceGrid(dimension);
        }

        public event EventHandler<SpaceGridScaledEventArgs> SpaceGridScaled;
        public event EventHandler SpaceGridReset;
        public event EventHandler StepBufferApplied;

        protected virtual void OnSpaceGridScaled(SpaceGridScaledEventArgs e)
        {
            var handler = SpaceGridScaled;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnSpaceGridReset(EventArgs e)
        {
            var handler = SpaceGridReset;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnStepBufferApplied(EventArgs e)
        {
            var handler = StepBufferApplied;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public bool IsCellAlive(int i, int j)
        {
            return _spaceGrid[i][j];
        }
        public void SetCellLivingState(int i, int j, bool state)
        {
            _spaceGrid[i][j] = state;
        }

        private BitArray[] extendSpaceGrid(int newDimension)
        {
            BitArray[] newSpaceGrid = createSpaceGrid(newDimension);

            int offset = (newDimension - Dimension) / 2;

            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    newSpaceGrid[i + offset][j + offset] = _spaceGrid[i][j];
                }
            }

            return newSpaceGrid;
        }
        private BitArray[] reduceSpaceGrid(int newDimension)
        {
            BitArray[] newSpaceGrid = createSpaceGrid(newDimension);

            int offset = (Dimension - newDimension) / 2;

            for (int i = 0; i < newDimension; i++)
            {
                for (int j = 0; j < newDimension; j++)
                {
                    newSpaceGrid[i][j] = _spaceGrid[i + offset][j + offset];
                }
            }

            return newSpaceGrid;
        }

        public void Scale(int newDimension)
        {
            if (newDimension < Dimension)
            {
                _spaceGrid = reduceSpaceGrid(newDimension);
            }
            else if (newDimension > Dimension)
            {
                _spaceGrid = extendSpaceGrid(newDimension);
            }

            OnSpaceGridScaled(new SpaceGridScaledEventArgs { NewDimension = Dimension });
        }

        public void Clear()
        {
            foreach (var row in _spaceGrid)
            {
                row.Xor(row);
            }

            OnSpaceGridReset(EventArgs.Empty);
        }

        public int CountAliveCells()
        {
            int count = 0;

            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    if (_spaceGrid[i][j] == true)
                    {
                        ++count;
                    }
                }
            }

            return count;
        }

        public void ApplyStepBuffer(BitArray[] stepBuffer)
        {
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    _spaceGrid[i][j] = stepBuffer[i][j];
                }
            }

            OnStepBufferApplied(EventArgs.Empty);
        }

        public string AsString()
        {
            var strBuilder = new StringBuilder(Dimension * Dimension + Dimension);

            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    strBuilder.Append(_spaceGrid[i][j] == true ? '1' : '*');
                }

                strBuilder.AppendLine();
            }

            return strBuilder.ToString();
        }
    }
}
