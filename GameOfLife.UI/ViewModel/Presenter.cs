using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

using GameOfLife.Core;
using GameOfLife.UI.Properties;
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
        private GameAnimationHelper         _gameAnimationHelper;
        
        private Rect                        _gridConfig;
        private string                      _startStopBtnText;

        public Rect     GridConfig
        {
            get {
                return _gridConfig;
            }
            set {
                _gridConfig = value;
                RaisePropertyChangedEvent("GridConfig");
            }
        }
        public string   StartStopBtnText
        {
            get {
                return _startStopBtnText;
            }
            set {
                _startStopBtnText = value;
                RaisePropertyChangedEvent("StartStopBtnText");
            }
        }

        public int SpaceGridSideLength 
        {
            get {
                return AppConsts.SpaceGridSideLength;
            }
        }

        public GameStatistics Statistics
        {
            get { 
                return _game.GameStatistics; 
            }
        }

        // Commands.
        public DelegateCommand<Canvas> StartBtnPressCommand { get; private set; }
        public DelegateCommand<Canvas> ResetBtnPressCommand { get; private set; }

        public DelegateCommand<Canvas> AddKeyPressCommand { get; private set; }
        public DelegateCommand<Canvas> SubtractKeyPressCommand { get; private set; }

        public DelegateCommand<MouseWheelEventArgs> MouseWheelCommand { get; private set; }
        public DelegateCommand<MouseEventArgs> MouseLeftButtonDownCommand { get; private set; }


        // C-tor.
        public Presenter()
        {
            _zoomHelper                 = new ZoomHelper();
            _game                       = new Game(_zoomHelper.CurrentDimension);
            _spaceGridInteractionHelper = new SpaceGridInteractionHelper(_zoomHelper, _game);
            _gameAnimationHelper        = new GameAnimationHelper(_game, _spaceGridInteractionHelper);

            GridConfig          = _zoomHelper.InitialGridConfig;
            StartStopBtnText    = Resources.StartBtnText;

            _game.GameStatistics.AliveCellsCountChanged += 
                (sender, e) => {
                    RaisePropertyChangedEvent("Statistics");
                };

            _game.GameStatistics.GenerationNumberChanged +=
                (sender, e) => {
                    RaisePropertyChangedEvent("Statistics");
                };

            initializeCommands();
        }

        private void initializeCommands()
        {
            StartBtnPressCommand = createStartBtnPressCommand();
            ResetBtnPressCommand = createResetBtnPressCommand();

            AddKeyPressCommand      = createAddKeyPressCommand();
            SubtractKeyPressCommand = createSubtractKeyPressCommand();

            MouseWheelCommand           = createMouseWheelCommand();
            MouseLeftButtonDownCommand  = createMouseLeftButtonDownCommand();
        }

        private DelegateCommand<Canvas> createStartBtnPressCommand()
        {
            return new DelegateCommand<Canvas>(
                    (canvas) => {
                        _gameAnimationHelper.Toggle(canvas);
                        StartStopBtnText =
                            _gameAnimationHelper.IsRunningNow ? 
                                Resources.StopBtnText : 
                                Resources.StartBtnText;

                        ResetBtnPressCommand.RaiseCanExecuteChanged();
                    });
        }
        private DelegateCommand<Canvas> createResetBtnPressCommand()
        {
            return new DelegateCommand<Canvas>(
                    canvas => {
                        _game.Reset();
                        canvas.Children.Clear();
                    },
                    () => !_gameAnimationHelper.IsRunningNow);
        }
        private DelegateCommand<Canvas> createAddKeyPressCommand()
        {
            return new DelegateCommand<Canvas>(
                    canvas => {
                        if (!_gameAnimationHelper.IsRunningNow)
                        {
                            zoom(canvas, 1);
                        }
                    });
        }
        private DelegateCommand<Canvas> createSubtractKeyPressCommand()
        {
            return new DelegateCommand<Canvas>(
                canvas => {
                    if (!_gameAnimationHelper.IsRunningNow)
                    {
                        zoom(canvas, -1);
                    }
                });
        }
        private DelegateCommand<MouseWheelEventArgs> createMouseWheelCommand()
        {
            return new DelegateCommand<MouseWheelEventArgs>(
                    param => {
                        if (!_gameAnimationHelper.IsRunningNow)
                        {
                            int zoomDirection = (param.Delta < 0) ? -1 : 1;
                            var sourceType = param.Source.GetType();

                            if (sourceType == typeof(Canvas))
                            {
                                zoom(param.Source as Canvas, zoomDirection);
                            }
                            else if (sourceType == typeof(Rectangle))
                            {
                                var sourceRect = param.Source as Rectangle;
                                zoom(sourceRect.Parent as Canvas, zoomDirection);
                            }
                        }
                    });
        }
        private DelegateCommand<MouseEventArgs> createMouseLeftButtonDownCommand()
        {
            return new DelegateCommand<MouseEventArgs>(
                    param => {
                        if (!_gameAnimationHelper.IsRunningNow)
                        {
                            var senderType = param.Source.GetType();

                            if (senderType == typeof(Canvas))
                                _spaceGridInteractionHelper.AddNewAliveCell(param);
                            else if (senderType == typeof(Rectangle))
                                _spaceGridInteractionHelper.RemoveAliveCell(param);
                        }
                    });
        }
        
        private void zoom(Canvas canvas, int zoomDirection)
        {
            canvas.Children.Clear();

            GridConfig = _zoomHelper.Apply(zoomDirection, GridConfig);
            _game.ScaleSpaceGrid(_zoomHelper.CurrentDimension);

            _spaceGridInteractionHelper.DrawCells(canvas);
        }
    }
}
