using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Engine
{
    /// <summary>
    /// Data structure containing information about a game object's position, rotation and scale
    /// </summary>
    public class Transformation
    {
        #region Data memebers

        /// <summary>
        /// Object's position in the world
        /// </summary>
        private Point mPosition;

        /// <summary>
        /// Object's clock-wise rotation angle
        /// </summary>
        private double mRotationAngle;

        /// <summary>
        /// Object's scale
        /// </summary>
        private Point mScale;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the position of the game object in the world
        /// </summary>
        public Point Position
        {
            get { return mPosition; }
            set { mPosition = value.X < 0.0 || value.Y < 0.0 ? new Point(0.0, 0.0) : value; }
        }

        /// <summary>
        /// Gets or sets the clock-wise rotation of the game object in the world
        /// </summary>
        public double RotationAngle
        {
            get { return mRotationAngle; }
            set { mRotationAngle = value % 360.0; }
        }

        /// <summary>
        /// Gets and sets the object's scale 
        /// </summary>
        public Point Scale
        {
            get { return mScale; }
            set { mScale = value.X == 0 || value.Y == 0 ? new Point(1.0, 1.0) : value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a transform with specific position, rotation and scale
        /// </summary>
        /// <param name="position">The object's position in the world</param>
        /// <param name="rotationAngle">The object's clock-wise rotation angle in the world</param>
        /// <param name="scale">The object's scale</param>
        public Transformation(Point position, double rotationAngle, Point scale)
        {
            Position = position;
            RotationAngle = rotationAngle;
            Scale = scale;
        }

        /// <summary>
        /// Creates a transform with specific position and rotation and a default scale of (1.0, 1.0)
        /// </summary>
        /// <param name="position">The object's position in the world</param>
        /// <param name="rotationAngle">The object's clock-wise rotation angle in the world</param>
        public Transformation(Point position, double rotationAngle)
        {
            Position = position;
            RotationAngle = rotationAngle;
            Scale = new Point(1.0, 1.0);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a matrix from the position, rotation and scale
        /// </summary>
        /// <returns></returns>
        public Transform GetTransformationMatrix()
        {
            TransformGroup group = new TransformGroup();
            group.Children.Add(new ScaleTransform(mScale.X, mScale.Y));
            group.Children.Add(new RotateTransform(mRotationAngle));
            group.Children.Add(new TranslateTransform(mPosition.X, mPosition.Y));

            return group;
        }

        #endregion
    }
}
