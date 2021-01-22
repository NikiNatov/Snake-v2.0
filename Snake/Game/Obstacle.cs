using System.Windows;
using Engine;

namespace SnakeGame
{
    /// <summary>
    /// Represents a collidable object in the game world
    /// </summary>
    public class Obstacle : GameObject
    {
        #region Constructors

        /// <summary>
        /// Creates an obstacle object that the snake can collide with
        /// </summary>
        /// <param name="width">Obstacle width</param>
        /// <param name="height">Obstacle height</param>
        /// <param name="texture">Obstacle texture</param>
        /// <param name="position">Obstacle position in the world</param>
        /// <param name="rotationAngle">Obstacle rotation in the world</param>
        /// <param name="scale">Obstacle scale</param>
        public Obstacle(int width, int height, Texture texture, Point position, double rotationAngle, Point scale)
            : base(width, height, texture, position, rotationAngle, scale)
        {
            Type = GameObjectType.Obstacle;
        }

        /// <summary>
        /// General use constructor for creating an obstacle object with specified texture and transformation
        /// </summary>
        /// <param name="width">Obstacle width</param>
        /// <param name="height">Obstacle height</param>
        /// <param name="texture">The texture applied to the obstacle</param>
        /// <param name="transformation">The transformation of the obstacle</param>
        public Obstacle(int width, int height, Texture texture, Transformation transformation)
            : base(width, height, texture, transformation)
        {
            Type = GameObjectType.Obstacle;
        }
        #endregion
    }
}
