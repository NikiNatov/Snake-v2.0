using System.Windows;

namespace Engine
{
    /// <summary>
    /// A static class containing helper functions for logging info, warnings or errors
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Spawns a message box for displaying specific information
        /// </summary>
        /// <param name="message">The message to be displayed</param>
        /// <param name="caption">The title of the message box</param>
        public static void LogInfo(string message, string caption = "Info")
            => MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);

        /// <summary>
        /// Spawns a message box for displaying specific warning
        /// </summary>
        /// <param name="message">The message to be displayed</param>
        /// <param name="caption">The title of the message box</param>
        public static void LogWarning(string message, string caption = "Warning")
            => MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);

        /// <summary>
        /// Spawns a message box for displaying specific error
        /// </summary>
        /// <param name="message">The message to be displayed</param>
        /// <param name="caption">The title of the message box</param>
        public static void LogError(string message, string caption = "Error")
            => MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
