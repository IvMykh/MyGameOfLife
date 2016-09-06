using System;
using System.Windows.Input;

namespace GameOfLife.UI.ViewModel.Infrastructure
{
    public class DelegateCommand<T>
        : ICommand 
        where T : class
    {
        private readonly Action<T>  _action;
        private readonly Func<bool> _canExecutePredicate;

        public DelegateCommand(Action<T> action, Func<bool> canExecutePredicate = null)
        {
            _action = action;
            _canExecutePredicate = canExecutePredicate;
        }

        public event EventHandler CanExecuteChanged;

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            var handler = CanExecuteChanged;
            
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecutePredicate == null ? true : _canExecutePredicate();
        }

        public void Execute(object parameter)
        {
            _action(parameter as T);
        }
    }
}
