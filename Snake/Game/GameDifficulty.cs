namespace SnakeGame
{
    /// <summary>
    /// Represents all difficulty levels in the game
    /// </summary>
    public enum GameDifficulty
    {
        /// <summary>
        /// Easy - less obstacles, more lifetime of the fruits
        /// </summary>
        Easy,

        /// <summary>
        /// Medium - more obstacles, with the same fruit lifetime as easy
        /// </summary>
        Medium,

        /// <summary>
        /// Hard - a lot of obstacles with less fruit lifetime
        /// </summary>
        Hard
    }
}
