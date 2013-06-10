using System;

namespace Naovigate.Navigation
{
    /// <summary>
    /// A simple direction.
    /// </summary>
    public enum Direction
    {
        Up = 0,
        Down = 2,
        Left = 3,
        Right = 1
    }

    static class DirectionExtension
    {
        /// <summary>
        /// Convert this direction to degrees in the range [0, 360)
        /// </summary>
        public static float ToAngle(this Direction dir)
        {
            if (dir == Direction.Up)
                return 0.0f;
            if (dir == Direction.Right)
                return 90.0f;
            if (dir == Direction.Down)
                return 180.0f;
            if (dir == Direction.Left)
                return 270.0f;
            throw new ArgumentException("Direction is not a valid direction.");
        }

        /// <summary>
        /// Convert this direction to radians in the range [-pi, pi].
        /// </summary>
        public static double ToRadian(this Direction dir)
        {
            double multiplier = 0.0;
            if (dir == Direction.Up)
                multiplier = -1.0;
            else if (dir == Direction.Right)
                multiplier = -0.5;
            else if (dir == Direction.Down)
                multiplier = 0;
            else if (dir == Direction.Left)
                multiplier = 0.5;
            else
                throw new ArgumentException("Direction is not a valid direction.");
            return multiplier * Math.PI;
        }
    }
}
