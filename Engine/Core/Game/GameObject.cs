using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Engine
{
    /// <summary>
    /// Class representing a base game object in the game world. The intended functionality for this class is
    /// to be inherited when creating object for the specific type of game the user is creating.
    /// </summary>
    public abstract class GameObject : INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when a property changes its value
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Data members

        /// <summary>
        /// Object type
        /// </summary>
        protected GameObjectType mType;

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
        public GameObjectType Type
        {
            get { return mType; }
            protected set
            {
                if(value != mType)
                {
                    mType = value;
                    OnPropertyChanged(nameof(Type));
                }
            }
        }

        /// <summary>
        /// Gets or sets the Texture of the game object
        /// </summary>
        public Texture Texture
        {
            get { return mTexture; }
            set 
            {
                if (value != mTexture)
                {
                    mTexture = value != null ? value : TextureManager.GetTexture("white_texture.png");
                    OnPropertyChanged(nameof(Texture));
                }
            }
        }

        /// <summary>
        /// Gets and sets the object's transformation
        /// </summary>
        public Transformation Transformation
        {
            get { return mTransformation; }
            set 
            {
                if (value != mTransformation)
                {
                    mTransformation = value != null ? value : new Transformation(new Point(0.0, 0.0), 0);
                    OnPropertyChanged(nameof(Transformation));
                }
            }
        }

        /// <summary>
        /// Gets and sets the width of the object
        /// </summary>
        public int Width
        {
            get { return mWidth; }
            set 
            {
                if (value != mWidth)
                {
                    mWidth = value > 0 ? value : 1;
                    OnPropertyChanged(nameof(Width));
                }
            }
        }

        /// <summary>
        /// Gets and sets the height of the object
        /// </summary>
        public int Height
        {
            get { return mHeight; }
            set 
            {
                if (value != mHeight)
                {
                    mHeight = value > 0 ? value : 1;
                    OnPropertyChanged(nameof(Height));
                }
            }
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
            Rect bb1 = GameMath.CalculateBoundingBox(this); 
            Rect bb2 = GameMath.CalculateBoundingBox(gameObject); 
            
            return bb1.IntersectsWith(bb2);
        }

        #endregion
    }
}
