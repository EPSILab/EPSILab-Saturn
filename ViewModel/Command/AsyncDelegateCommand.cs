using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SolarSystem.Saturn.ViewModel.Command
{
    class AsyncDelegateCommand<T> : ICommand
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Func<T, Task> _execute;
        private bool _isRunning;

        public AsyncDelegateCommand(Func<T, Task> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute ?? (x => true);
        }

        public void RaiseCanExecuteChange()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            bool testValue;

            if (parameter != null && bool.TryParse(parameter.ToString(), out testValue))
                parameter = testValue;

            return !_isRunning && _canExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged;

        public async void Execute(object parameter)
        {
            _isRunning = true;

            try
            {
                RaiseCanExecuteChange();

                bool testValue;

                if (bool.TryParse(parameter.ToString(), out testValue))
                    parameter = testValue;
                
                await _execute((T) parameter);
            }
            finally
            {
                _isRunning = false;
                RaiseCanExecuteChange();
            }
        }
    }

    class AsyncDelegateCommand : ICommand
    {
        private readonly Func<bool> _canExecute;
        private readonly Func<Task> _execute;
        private bool _isRunning;

        public AsyncDelegateCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute ?? (() => true);
        }

        public void RaiseCanExecuteChange()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return !_isRunning && _canExecute();
        }

        public event EventHandler CanExecuteChanged;

        public async void Execute(object parameter)
        {
            _isRunning = true;
            try
            {
                RaiseCanExecuteChange();
                await _execute();
            }
            finally
            {
                _isRunning = false;
                RaiseCanExecuteChange();
            }
        }
    }
}
