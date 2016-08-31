using System.Windows;

using GameOfLife.Core;

namespace GameOfLife.UI.ViewModel.Helpers
{
    class SpaceGridInteractionHelper
    {
        private ZoomHelper _zoomHelper;
        private Game       _game;

        public SpaceGridInteractionHelper(ZoomHelper zoomHelper, Game game)
        {
            _zoomHelper = zoomHelper;
            _game       = game;
        }

        public void ToggleCell(Point cursorPos)
        {
            int i = (int)cursorPos.Y / _zoomHelper.CurrentSquareSideLength;
            int j = (int)cursorPos.X / _zoomHelper.CurrentSquareSideLength;

            MessageBox.Show(string.Format("i={0}, j={1}", i, j));

            _game.SetCellLivingState(i, j, !_game.IsCellAlive(i, j));
        }
    }
}
