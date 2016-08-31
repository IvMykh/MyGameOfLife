using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameOfLife.UI.ViewModel.Helpers
{
    class ZoomHelper
    {
        // Static members.
        private static List<int> _divisors;

        static ZoomHelper()
        {
            _divisors = new List<int>();

            for (int i = AppConsts.MinCellSquareLength; i <= AppConsts.SpaceGridSideLength; i++)
            {
                if (AppConsts.SpaceGridSideLength % i == 0)
                {
                    _divisors.Add(i);
                }
            }
        }

        // Instance members.
        private int _currDivisorsIndex = 0;

        public int CurrentDimension { get; private set; }

        public int CurrentSquareSideLength
        {
            get {
                return _divisors[_currDivisorsIndex];
            }
        }

        public ZoomHelper()
        {
            int squareSide = _divisors[_currDivisorsIndex];
            CurrentDimension = AppConsts.SpaceGridSideLength / squareSide;
        }

        public Rect Apply(int zoomDirection, Rect oldGridConfig)
        {
            int newIndex = _currDivisorsIndex + zoomDirection;

            if (0 <= newIndex && newIndex < _divisors.Count)
            {
                _currDivisorsIndex = newIndex;
                int newSquareSide = _divisors[_currDivisorsIndex];
                CurrentDimension = AppConsts.SpaceGridSideLength / newSquareSide;

                return new Rect(0, 0, newSquareSide, newSquareSide);
            }
            else
            {
                return oldGridConfig;
            }
        }

        public Rect InitialGridConfig
        {
            get
            {
                int squareSide = _divisors[_currDivisorsIndex];
                return new Rect(0, 0, squareSide, squareSide);
            }
        }
    }
}
