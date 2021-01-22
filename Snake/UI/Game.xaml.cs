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
using System.Windows.Threading;

namespace SnakeGame.UI
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : UserControl
    {
        #region Dependency properties

        /// <summary>
        /// Command data for the <see cref="MouseMoveCommandProperty"/>
        /// </summary>
        public ICommand MouseMoveCommand
        {
            get { return (ICommand)GetValue(MouseMoveCommandProperty); }
            set { SetValue(MouseMoveCommandProperty, value); }
        }

        /// <summary>
        /// Defines a property for a mouse moved command
        /// </summary>
        public static readonly DependencyProperty MouseMoveCommandProperty 
            = DependencyProperty.Register("MouseMoveCommand", typeof(ICommand), typeof(Game), new PropertyMetadata(null));

        /// <summary>
        /// Command data for the <see cref="KeyPressedCommandProperty"/>
        /// </summary>
        public ICommand KeyPressedCommand
        {
            get { return (ICommand)GetValue(KeyPressedCommandProperty); }
            set { SetValue(KeyPressedCommandProperty, value); }
        }

        /// <summary>
        /// Defines a property for a key pressed command
        /// </summary>
        public static readonly DependencyProperty KeyPressedCommandProperty
            = DependencyProperty.Register("KeyPressedCommand", typeof(ICommand), typeof(Game), new PropertyMetadata(null));

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a game user control
        /// </summary>
        public Game()
        {
            InitializeComponent();
            Application.Current.MainWindow.KeyDown += OnKeyPressed;
        }

        #endregion

        #region Events

        /// <summary>
        /// Called when the mouse is moved
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event arguments</param>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            MouseMoveCommand?.Execute(new Point(e.GetPosition((UserControl)sender).X - 16, e.GetPosition((UserControl)sender).Y - 32));
        }

        /// <summary>
        /// Called when a key is pressed
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event argumments</param>
        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            KeyPressedCommand?.Execute(e.Key);
        }

        #endregion


    }
}
