using System.Windows;
using Engine;

namespace SnakeGame
{
    /// <summary>
    /// Represents a fruit eatable by the snake
    /// </summary>
    public class Fruit : GameObject
    {
        #region Data members

        /// <summary>
        /// Score points data
        /// </summary>
        private int  mScorePoints;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the score points the player is rewarded by picking the fruit
        /// </summary>
        public int ScorePoints
        {
            get { return mScorePoints; }
            set { mScorePoints = value >= 0 ? value : 0; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// General use constructor that initialized a fruit with specified score points, texture, position and rotation
        /// </summary>
        /// <param name="scorePoints">The amount of score points the fruit gives</param>
        /// <param name="texture">The texture of the fruit</param>
        /// <param name="position">The position of the fruit's center point on the canvas</param>
        /// <param name="rotationAngle">The rotation angle of the fruit</param>
        /// <param name="scale">The sclae of the fruit</param>
        public Fruit(int scorePoints, int width, int height, Texture texture, Point position, double rotationAngle, Point scale)
            : base(width, height, texture, position, rotationAngle, scale)
        {
            Type = GameObjectType.Fruit;
            ScorePoints = scorePoints;
        }

        /// <summary>
        /// General use constructor for creating a fruit with specified score points, width, height, texture and transformation
        /// </summary>
        /// <param name="scorePoints">The amount of score points the fruit gives</param>
        /// <param name="width">The width of the fruit</param>
        /// <param name="height">The height of the fruit</param>
        /// <param name="texture">The texture applied to the fruit</param>
        /// <param name="transformation">The transformation of the fruit</param>
        protected Fruit(int scorePoints, int width, int height, Texture texture, Transformation transformation)
            : base(width, height, texture, transformation)
        {
            Type = GameObjectType.Fruit;
            ScorePoints = scorePoints;
        }

        #endregion
    }
}
