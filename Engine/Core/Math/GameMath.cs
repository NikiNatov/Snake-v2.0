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
    /// Helper class containing useful mathematical functions for game programming
    /// </summary>
    public static class GameMath
    {
        /// <summary>
        /// Function for moving from a position to a target point. The distance covered is determined by the movement speed.
        /// </summary>
        /// <param name="target">The target/destination point</param>
        /// <param name="current">The current position</param>
        /// <param name="movementSpeed">The movement speed</param>
        /// <param name="distanceFromTarget">The value used to determine when to stop the movement. The distance covered will never be bigger than this value.</param>
        /// <returns>The new position after moving</returns>
        public static Point MoveTowards(Point target, Point current, double movementSpeed, double distanceFromTarget)
        {
            Vector direction = target - current;
            double length = direction.Length;

            return length <= distanceFromTarget ? current : current + direction / length * movementSpeed;
        }

        /// <summary>
        /// Generate a bouning box for the object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Rect CalculateBoundingBox(GameObject obj)
        {
            TransformGroup transform = new TransformGroup();
            transform.Children.Add(new RotateTransform(obj.Transformation.RotationAngle, obj.Width / 2, obj.Height / 2));
            transform.Children.Add(new TranslateTransform(obj.Transformation.Position.X, obj.Transformation.Position.Y));

            Point tl1 = transform.Value.Transform(new Point(6, 6));
            Point tr1 = transform.Value.Transform(new Point(obj.Width - 6, 6));
            Point bl1 = transform.Value.Transform(new Point(6, obj.Height - 6));
            Point br1 = transform.Value.Transform(new Point(obj.Width - 6, obj.Height - 6));

            double minX1 = Math.Min(tl1.X, Math.Min(tr1.X, Math.Min(bl1.X, br1.X)));
            double maxX1 = Math.Max(tl1.X, Math.Max(tr1.X, Math.Max(bl1.X, br1.X)));
            double minY1 = Math.Min(tl1.Y, Math.Min(tr1.Y, Math.Min(bl1.Y, br1.Y)));
            double maxY1 = Math.Max(tl1.Y, Math.Max(tr1.Y, Math.Max(bl1.Y, br1.Y)));
            Point min1 = new Point(minX1, minY1);
            Point max1 = new Point(maxX1, maxY1);

            return new Rect(min1, max1 - min1);
        }
    }
}
