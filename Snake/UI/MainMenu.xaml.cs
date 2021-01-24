using System.Windows;
using System.Windows.Controls;

namespace SnakeGame.UI
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        /// <summary>
        /// Base constructor
        /// </summary>
        public MainMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event for clicking the Change Difficulty button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChangeDifficultyPress(object sender, RoutedEventArgs e)
        {
            if(DifficultyWindow.Visibility == Visibility.Collapsed)
                DifficultyWindow.Visibility = Visibility.Visible;
            else
                DifficultyWindow.Visibility = Visibility.Collapsed;

            HelperWindow.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Event for clicking the Helper button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHelperPress(object sender, RoutedEventArgs e)
        {
            if (HelperWindow.Visibility == Visibility.Collapsed)
                HelperWindow.Visibility = Visibility.Visible;
            else
                HelperWindow.Visibility = Visibility.Collapsed;

            DifficultyWindow.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Event for closing the Change Difficulty pop-up window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDifficultyWindowClose(object sender, RoutedEventArgs e)
        {
            DifficultyWindow.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Event for closing the helper pop-up window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHelperWindowClose(object sender, RoutedEventArgs e)
        {
            HelperWindow.Visibility = Visibility.Collapsed;
        }
    }
}
