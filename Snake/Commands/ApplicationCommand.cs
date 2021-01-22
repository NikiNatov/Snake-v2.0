using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SnakeGame
{
    public class ApplicationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action mAction;

        public ApplicationCommand(Action action)
        {
            mAction = action;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            mAction();
        }
    }
}
