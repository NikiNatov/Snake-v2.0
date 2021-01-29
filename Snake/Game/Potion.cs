using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Engine;

namespace SnakeGame
{
    /// <summary>
    /// All possible potion effects
    /// </summary>
    public enum PotionEffect 
    {
        /// <summary>
        /// No effect
        /// </summary>
        None,

        /// <summary>
        /// Shields the snake
        /// </summary>
        Shield,

        /// <summary>
        /// Doubles the points obtained from eating a fruit
        /// </summary>
        PointsX2
    }

    /// <summary>
    /// Represents a potion with certain effect
    /// </summary>
    public class Potion : GameObject
    {
        #region Data members

        /// <summary>
        /// Potion effect
        /// </summary>
        private PotionEffect mEffect;

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets the effect of the potion
        /// </summary>
        public PotionEffect Effect
        {
            get { return mEffect; }
            set
            {
                if(value != mEffect)
                {
                    mEffect = value;
                    OnPropertyChanged(nameof(Effect));
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// General use constructor that initialized a potion with specified effect, texture, position, rotation and scale
        /// </summary>
        /// <param name="effect">The effect of the potion</param>
        /// <param name="texture">The texture of the potion</param>
        /// <param name="position">The position of the potion's center point on the canvas</param>
        /// <param name="rotationAngle">The rotation angle of the potion</param>
        /// <param name="scale">The sclae of the potion</param>
        public Potion(PotionEffect effect, int width, int height, Texture texture, Point position, double rotationAngle, Point scale)
            : base(width, height, texture, position, rotationAngle, scale)
        {
            Effect = effect;
            Type = GameObjectType.Potion;
        }

        /// <summary>
        /// General use constructor for creating a potion with specified effect, width, height, texture and transformation
        /// </summary>
        /// <param name="effect">The effect of the potion</param>
        /// <param name="width">The width of the potion</param>
        /// <param name="height">The height of the potion</param>
        /// <param name="texture">The texture applied to the potion</param>
        /// <param name="transformation">The transformation of the potion</param>
        public Potion(PotionEffect effect, int width, int height, Texture texture, Transformation transformation)
            : base(width, height, texture, transformation)
        {
            Effect = effect;
            Type = GameObjectType.Potion;
        }

        #endregion
    }
}
