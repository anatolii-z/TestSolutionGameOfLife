using System;
using System.Windows.Input;

namespace TestSolutionGameOfLife
{
    internal class RelayCommand : ICommand
    {
        private Action<string> _execute;
        private Func<bool> _canExecute;
            
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<string> execute, Func<bool> canExecute = null)
        {
             _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _execute((string)parameter);
        }
    }
}
