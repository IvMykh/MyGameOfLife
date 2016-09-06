using System;
using System.Threading;
using System.Windows.Controls;
using GameOfLife.Core;

namespace GameOfLife.UI.ViewModel.Helpers
{
    class GameAnimationHelper
        : IDisposable
    {
        private Timer                       _timer;
        private Game                        _game;
        private SpaceGridInteractionHelper  _spaceGridInteractionHelper;

        public bool IsRunningNow { get; private set; }

        public GameAnimationHelper(
            Game game, SpaceGridInteractionHelper spaceGridInteractionHelper)
        {
            _game = game;
            _spaceGridInteractionHelper = spaceGridInteractionHelper;
            IsRunningNow = false;
        }

        private void animate(object param)
        {
            _game.PerformStep();
                    
             var canvas = param as Canvas;
             canvas.Dispatcher.Invoke(
                 () => { 
                     canvas.Children.Clear(); 
                     _spaceGridInteractionHelper.DrawCells(canvas);
                 });
        }

        public void Toggle(Canvas canvas)
        {
            if (_timer == null)
            {
                _timer = new Timer(animate, canvas, 50, 50);
                IsRunningNow = true;
            }
            else
            {
                Dispose();
                IsRunningNow = false;
            }
        }
    
        public void Dispose()
        {
            _timer.Dispose();
            _timer = null;
        }
    }
}
