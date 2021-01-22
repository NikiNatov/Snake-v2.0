using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Engine
{
    /// <summary>
    /// Class representing a base game object in the game world. The intended functionality for this class is
    /// to be inherited when creating object for the specific type of game the user is creating.
    /// </summary>
    public abstract class GameObject
    {
        #region Data members

        /// <summary>
        /// Texture data
        /// </summary>
        protected Texture mTexture;

        /// <summary>
        /// Transformation data
        /// </summary>
        protected Transformation mTransformation;

        /// <summary>
        /// Width of the object
        /// </summary>
        protected int mWidth;

        /// <summary>
        /// Height of the object
        /// </summary>
        protected int mHeight;

        #endregion

        #region Properties

        /// <summary>
        /// Get or protected sets the type of the game object ( SnakeComponent, Fruit, Obstacle )
        /// </summary>
        public GameObjectType Type { get; protected set; }

        /// <summary>
        /// Gets or sets the Texture of the game object
        /// </summary>
        public Texture Texture
        {
            get { return mTexture; }
            set { mTexture = value != null ? value : TextureManager.GetTexture("white_texture.png"); }
        }

        /// <summary>
        /// Gets and sets the object's transformation
        /// </summary>
        public Transformation Transformation
        {
            get { return mTransformation; }
            set { mTransformation = value != null ? value : new Transformation(new Point(0.0, 0.0), 0); }
        }

        /// <summary>
        /// Gets and sets the width of the object
        /// </summary>
        public int Width
        {
            get { return mWidth; }
            set { mWidth = value > 0 ? value : 1; }
        }

        /// <summary>
        /// Gets and sets the height of the object
        /// </summary>
        public int Height
        {
            get { return mHeight; }
            set { mHeight = value > 0 ? value : 1; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// General use constructor for creating a game object with specified width, height, texture, position, rotation and scale
        /// </summary>
        /// <param name="width">The width of the object</param>
        /// <param name="height">The height of the object</param>
        /// <param name="texture">The texture applied to the game object</param>
        /// <param name="position">The location of the game object's center point on the canvas</param>
        /// <param name="rotationAngle">The clock-wise rotation angle of the game object relative to the center point of the game object</param>
        /// <param name="scale">The sclae of the game object</param>
        protected GameObject(int width, int height, Texture texture, Point position, double rotationAngle, Point scale)
        {
            Width = width;
            Height = height;
            Texture = texture;
            Transformation = new Transformation(position, rotationAngle, scale);
        }

        /// <summary>
        /// General use constructor for creating a game object object with specified width, height, texture and transformation
        /// </summary>
        /// <param name="width">The width of the object</param>
        /// <param name="height">The height of the object</param>
        /// <param name="texture">The texture applied to the game object</param>
        /// <param name="transformation">The transformation of the game object</param>
        protected GameObject(int width, int height, Texture texture, Transformation transformation)
        {
            Width = width;
            Height = height;
            Texture = texture;
            Transformation = transformation;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method for checking if two game objects bounding boxes intersect
        /// </summary>
        /// <param name="gameObject">The comparison game object</param>
        /// <returns>Returns true if there is a collision and false if there is not</returns>
        virtual public bool CollidesWith(GameObject gameObject)
        {
            Rect aabb1 = GameMath.CalculateBoundingBox(this); 
            Rect aabb2 = GameMath.CalculateBoundingBox(gameObject); 

            return aabb1.IntersectsWith(aabb2);
        }

        #endregion
    }
}
