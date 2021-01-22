using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SnakeGame
{
    public class KeyPressedCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<Key> mAction;

        public KeyPressedCommand(Action<Key> action)
        {
            mAction = action;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            mAction((Key)parameter);
        }
    }
}
