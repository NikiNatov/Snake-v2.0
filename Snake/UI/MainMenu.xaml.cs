using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnakeGame.UI
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void OnChangeDifficultyPress(object sender, RoutedEventArgs e)
        {
            if(DifficultyWindow.Visibility == Visibility.Collapsed)
                DifficultyWindow.Visibility = Visibility.Visible;
            else
                DifficultyWindow.Visibility = Visibility.Collapsed;
        }

        private void OnChangeHelperPress(object sender, RoutedEventArgs e)
        {
            if (HelperWindow.Visibility == Visibility.Collapsed)
                HelperWindow.Visibility = Visibility.Visible;
            else
                HelperWindow.Visibility = Visibility.Collapsed;
        }

        private void OnDifficultyWindowClose(object sender, RoutedEventArgs e)
        {
            DifficultyWindow.Visibility = Visibility.Collapsed;
        }

        private void OnHelperWindowClose(object sender, RoutedEventArgs e)
        {
            HelperWindow.Visibility = Visibility.Collapsed;
        }
    }
}
