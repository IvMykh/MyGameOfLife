using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using GameOfLife.UI.ViewModel.Helpers;
using GameOfLife.Core;

namespace GameOfLife.UI.ViewModel
{
    public class Presenter
        : ObservableObject
    {
        private Game                        _game;
        private ZoomHelper                  _zoomHelper;
        private SpaceGridInteractionHelper  _spaceGridInteractionHelper;

        private Rect _gridConfig;

        public Rect GridConfig
        {
            get {
                return _gridConfig;
            }
            set {
                _gridConfig = value;
                RaisePropertyChangedEvent("GridConfig");
            }
        }

        public int SpaceGridSideLength 
        {
            get {
                return AppConsts.SpaceGridSideLength;
            }
        }

        public Presenter()
        {
            _zoomHelper                 = new ZoomHelper();
            _game                       = new Game(_zoomHelper.CurrentDimension);
            _spaceGridInteractionHelper = new SpaceGridInteractionHelper(_zoomHelper, _game);

            GridConfig = _zoomHelper.InitialGridConfig;
        }

        private void zoom(int zoomDirection)
        {
            GridConfig = _zoomHelper.Apply(zoomDirection, GridConfig);
            _game.ScaleSpaceGrid(_zoomHelper.CurrentDimension);
        }

        public ICommand MouseWheelCommand
        {
            get {
                return new DelegateCommand<MouseWheelEventArgs>(
                    param => {
                        int zoomDirection = (param.Delta < 0) ? -1 : 1;
                        zoom(zoomDirection);
                    });
            }
        }

        public ICommand KeyPressCommand
        {
            get {
                return new DelegateCommand<string>(
                    param => {
                        zoom(int.Parse(param));
                    });
            }
        }

        public ICommand MouseLeftButtonDownCommand
        {
            get {
                return new DelegateCommand<MouseEventArgs>(
                    param => {
                        Point cursorPos = param.GetPosition(param.Source as Canvas);
                        _spaceGridInteractionHelper.ToggleCell(cursorPos);

                        // TODO: draw corresponding rectangle (see binding items list in MinimumMVVM).s
                    });
            }
        }
    }
}
