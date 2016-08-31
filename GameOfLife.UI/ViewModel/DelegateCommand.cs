using System;
using System.Windows.Input;

namespace GameOfLife.UI.ViewModel
{
    public class DelegateCommand<T>
        : ICommand 
        where T : class
    {
        private readonly Action<T> _action;

        public DelegateCommand(Action<T> action)
        {
            _action = action;
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
            return true;
        }

        public void Execute(object parameter)
        {
            _action(parameter as T);
        }
    }
}
