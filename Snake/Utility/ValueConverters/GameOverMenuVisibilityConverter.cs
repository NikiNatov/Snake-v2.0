using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SnakeGame
{
    /// <summary>
    /// Helper class for converting object visibility to game state and reverse
    /// </summary>
    public class GameOverMenuVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is GameState state)
            {
                switch (state)
                {
                    case GameState.PLAY:
                        return Visibility.Collapsed;
                    case GameState.PAUSE:
                        return Visibility.Collapsed;
                    case GameState.GAME_OVER:
                        return Visibility.Visible;
                }
            }

            throw new Exception("Invalid Game State!");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility vis)
            {
                switch (vis)
                {
                    case Visibility.Collapsed:
                        return GameState.PLAY;
                    case Visibility.Visible:
                        return GameState.GAME_OVER;
                }
            }

            throw new Exception("Invalid Visibility!");
        }
    }
}
