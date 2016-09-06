using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using GameOfLife.Core;

namespace GameOfLife.UI.ViewModel.Helpers
{
    class SpaceGridInteractionHelper
    {
        struct IndexPair
        {
            public int I { get; set; }
            public int J { get; set; }
        }

        private ZoomHelper _zoomHelper;
        private Game       _game;

        public SpaceGridInteractionHelper(ZoomHelper zoomHelper, Game game)
        {
            _zoomHelper = zoomHelper;
            _game       = game;
        }

        private IndexPair getCellIndexPair(Point cursorPos)
        {
            return new IndexPair { 
                I = (int)cursorPos.Y / _zoomHelper.CurrentSquareSideLength,
                J = (int)cursorPos.X / _zoomHelper.CurrentSquareSideLength
            };
        }
        private Point getCanvasDrawPoint(IndexPair cellIndexPair)
        {
            return new Point(
                cellIndexPair.J * _zoomHelper.CurrentSquareSideLength, 
                cellIndexPair.I * _zoomHelper.CurrentSquareSideLength
                );
        }

        public void AddNewAliveCell(MouseEventArgs e)
        {
            var canvas = e.Source as Canvas;

            Point cursorPos = e.GetPosition(canvas);

            var cellIndexPair = getCellIndexPair(cursorPos);
            _game.SetCellLivingState(cellIndexPair.I, cellIndexPair.J, true);

            drawCellOnCanvas(canvas, cellIndexPair);
        }
        public void RemoveAliveCell(MouseEventArgs e)
        {
            var rect = e.Source as Rectangle;
            var canvas = rect.Parent as Canvas;

            Point cursorPos = e.GetPosition(canvas);

            var cellIndexPair = getCellIndexPair(cursorPos);
            _game.SetCellLivingState(cellIndexPair.I, cellIndexPair.J, false);
            
            canvas.Children.Remove(rect);
        }

        private void drawCellOnCanvas(Canvas canvas, IndexPair cellIndexPair)
        {
            var rect = new Rectangle() {
                Width   = _zoomHelper.CurrentSquareSideLength,
                Height  = _zoomHelper.CurrentSquareSideLength,
                Fill    = Brushes.Black
            };

            var canvasDrawPos = getCanvasDrawPoint(cellIndexPair);

            Canvas.SetLeft(rect, canvasDrawPos.X);
            Canvas.SetTop(rect, canvasDrawPos.Y);

            canvas.Children.Add(rect);
        }
        
        public void DrawCells(Canvas canvas)
        {
            for (int i = 0; i < _zoomHelper.CurrentDimension; i++)
            {
                for (int j = 0; j < _zoomHelper.CurrentDimension; j++)
                {
                    if (_game.IsCellAlive(i, j))
                    {
                        drawCellOnCanvas(canvas, new IndexPair { I = i, J = j });
                    }
                }
            }
        }
    }
}
