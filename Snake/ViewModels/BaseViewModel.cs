using PropertyChanged;
using System.ComponentModel;

namespace SnakeGame
{
    /// <summary>
    /// Base view model that fires PropertyChanged events
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when a property down the hierarchy changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Calls the property changed event with a property name
        /// </summary>
        /// <param name="name">The property name</param>
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
