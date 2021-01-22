using System;
using System.Windows.Input;

namespace SnakeGame
{
    /// <summary>
    /// Command for changing the difficulty of the game
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Events

        /// <summary>
        /// Event that fires when the execution ability of the command changes
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Data members

        /// <summary>
        /// The function that has to be executed when the command is called
        /// </summary>
        public Action<object> mAction;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a command with an action method
        /// </summary>
        /// <param name="action"></param>
        public RelayCommand(Action<object> action)
        {
            mAction = action;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Function for enabling the execution of the command. A difficulty change command can always be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Triggers when the command is called
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction(parameter);
        }

        #endregion
    }
}
