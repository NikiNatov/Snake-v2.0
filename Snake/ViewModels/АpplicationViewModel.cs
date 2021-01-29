using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SnakeGame
{
    /// <summary>
    /// Class that runs the application logic
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {

        #region Data members

        /// <summary>
        /// Currently active view
        /// </summary>
        public BaseViewModel mCurrentView;

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets the currently displayed view
        /// </summary>
        public BaseViewModel CurrentView
        {
            get { return mCurrentView; }
            set
            {
                if (mCurrentView != value)
                {
                    mCurrentView = value;
                    OnPropertyChanged(nameof(CurrentView));
                }
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Command for switching to the main menu view
        /// </summary>
        public ICommand MainMenuCommand { get; set; }

        /// <summary>
        /// Command for switching to the game view
        /// </summary>
        public ICommand StartGameCommand { get; set; }

        /// <summary>
        /// Command for closing the application
        /// </summary>
        public ICommand ExitCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an application viewmodel
        /// </summary>
        public ApplicationViewModel()
        {
            CurrentView         = new MainMenuViewModel();
            MainMenuCommand     = new ApplicationCommand(GoToMenu);
            StartGameCommand    = new ApplicationCommand(StartGame);
            ExitCommand         = new ApplicationCommand(Exit);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the current view to the game view or restarts the game
        /// </summary>
        private void StartGame()
        {
            if (CurrentView is MainMenuViewModel mainMenu)
                CurrentView = new GameViewModel(mainMenu.DifficultyLevel);
        }

        /// <summary>
        /// Sets the current view to the main menu
        /// </summary>
        private void GoToMenu()
        {
            CurrentView = new MainMenuViewModel();
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        private void Exit()
        {
            Environment.Exit(0);
        }

        #endregion
    }
}
