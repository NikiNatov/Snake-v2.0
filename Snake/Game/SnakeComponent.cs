using System.Windows;
using Engine;

namespace SnakeGame
{
    /// <summary>
    /// Representes a piece of the snake body
    /// </summary>
    public class SnakeComponent : GameObject
    {
        /// <summary>
        /// Creates a snake component with specified width, height, texture, position, rotation angle
        /// </summary>
        /// <param name="width">Piece's width</param>
        /// <param name="height">Piece's height/param>
        /// <param name="texture">Piece's texture</param>
        /// <param name="position">Piece's position in the world</param>
        /// <param name="rotationAngle">Piece's rotation in the world</param>
        /// <param name="scale">Piece's scale</param>
        public SnakeComponent(int width, int height, Texture texture, Point position, double rotationAngle, Point scale)
            : base(width, height, texture, position, rotationAngle, scale)
        {
            Type = GameObjectType.SnakeComponent;
        }

        /// <summary>
        /// Creates a snake component with specified width, height, texture, transformation
        /// </summary>
        /// <param name="width">Piece's width</param>
        /// <param name="height">Piece's height</param>
        /// <param name="texture">Piece's texture</param>
        /// <param name="transformation">Piece's transformation</param>
        public SnakeComponent(int width, int height, Texture texture, Transformation transformation)
            : base(width, height, texture, transformation)
        {
            Type = GameObjectType.SnakeComponent;
        }
    }
}
