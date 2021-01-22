using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SnakeGame
{
    public class MouseMoveCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<Point> mAction;

        public MouseMoveCommand(Action<Point> action)
        {
            mAction = action;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            mAction((Point)parameter);
        }
    }
}
