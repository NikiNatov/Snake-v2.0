using System.Windows.Input;

namespace SnakeGame
{
    /// <summary>
    /// Class that runs main menu logic
    /// </summary>
    public class MainMenuViewModel : BaseViewModel
    {
        #region Data members

        /// <summary>
        /// Difficulty level
        /// </summary>
        private GameDifficulty mDifficultyLevel;

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets the game difficulty
        /// </summary>
        public GameDifficulty DifficultyLevel
        {
            get { return mDifficultyLevel; }
            set
            {
                if (mDifficultyLevel != value)
                {
                    mDifficultyLevel = value;
                    OnPropertyChanged(nameof(DifficultyLevel));
                }
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Calls a function for difficulty of the game 
        /// </summary>
        public ICommand ChangeDifficultyCommand { get; set; }
        
        #endregion

        #region Constructors

        /// <summary>
        /// Creates a main menu viewmodel
        /// </summary>
        public MainMenuViewModel()
        {
            DifficultyLevel = GameDifficulty.Easy;
            ChangeDifficultyCommand = new RelayCommand(OnDifficultyChanged);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method called when the difficulty buttons are clicked
        /// </summary>
        /// <param name="sender"></param>
        private void OnDifficultyChanged(object sender)
        {
            if (sender.ToString() == "Easy")
                DifficultyLevel = GameDifficulty.Easy;
            else if (sender.ToString() == "Medium")
                DifficultyLevel = GameDifficulty.Medium;
            else
                DifficultyLevel = GameDifficulty.Hard;
        }

        #endregion
    }
}
