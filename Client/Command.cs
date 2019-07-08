using System;
using System.Windows.Input;

namespace Client
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action action;

        public Command(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action?.Invoke();
        }
    }
}
