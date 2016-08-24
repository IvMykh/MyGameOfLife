﻿using System.Collections;
using System.Text;

namespace GameOfLife.Core
{
    internal class SpaceGrid
    {
        private BitArray[] _spaceGrid;

        public int Dimension
        {
            get
            {
                return _spaceGrid.Length;
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

        public void SetCellAlive(int i, int j)
        {
            _spaceGrid[i][j] = true;
        }
        public void SetCellDead(int i, int j)
        {
            _spaceGrid[i][j] = false;
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