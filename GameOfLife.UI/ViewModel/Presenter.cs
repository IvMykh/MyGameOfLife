using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

using GameOfLife.Core;
using GameOfLife.UI.ViewModel.Helpers;
using GameOfLife.UI.ViewModel.Infrastructure;

namespace GameOfLife.UI.ViewModel
{
    public class Presenter
        : ObservableObject
    {
        private Game                        _game;
        private ZoomHelper                  _zoomHelper;
        private SpaceGridInteractionHelper  _spaceGridInteractionHelper;
        
        private Rect    _gridConfig;

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

        private void zoom(Canvas canvas, int zoomDirection)
        {
            canvas.Children.Clear();

            GridConfig = _zoomHelper.Apply(zoomDirection, GridConfig);
            _game.ScaleSpaceGrid(_zoomHelper.CurrentDimension);

            _spaceGridInteractionHelper.DrawCells(canvas);
        }

        public ICommand MouseWheelCommand
        {
            get {
                return new DelegateCommand<MouseWheelEventArgs>(
                    param => {
                        // TODO: check whether param.Source as Canvas != null.

                        int zoomDirection = (param.Delta < 0) ? -1 : 1;
                        zoom(param.Source as Canvas, zoomDirection);
                    });
            }
        }

        public ICommand AddKeyPressCommand
        {
            get {
                return new DelegateCommand<Canvas>(
                    canvas => {
                        zoom(canvas, 1);
                    });
            }
        }
        public ICommand SubtractKeyPressCommand
        {
            get {
                return new DelegateCommand<Canvas>(
                    canvas => {
                        zoom(canvas, -1);
                    });
            }
        }

        public ICommand MouseLeftButtonDownCommand
        {
            get {
                return new DelegateCommand<MouseEventArgs>(
                    param => {
                        var senderType = param.Source.GetType();

                        if (senderType == typeof(Canvas))
                            _spaceGridInteractionHelper.AddNewAliveCell(param);
                        else if (senderType == typeof(Rectangle))
                            _spaceGridInteractionHelper.RemoveAliveCell(param);

                    });
            }
        }

        public ICommand ResetBtnPressCommand
        {
            get {
                return new DelegateCommand<Canvas>(
                    canvas => {
                        _game.Reset();
                        canvas.Children.Clear();
                    });
            }
        }
    }
}
