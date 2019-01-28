using System;
using System.Collections.Generic;
using System.Text;

namespace NearestNeighbour
{
    public class AABB
    {
        public Point Center { get; set; }
        public Point Half { get; set; }

        public AABB(Point center, Point half)
        {
            Center = center;
            Half = half;
        }

        /// <summary>
        /// Determines if a point is within the AABB
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Contains(Point p)
        {
            return (p.X >= Center.X - Half.X && p.Y >= Center.Y - Half.Y && p.X <= Center.X + Half.X && p.Y <= Center.Y + Half.Y);
        }

        /// <summary>
        /// Determines if two AABB intersect by determining if the one of the corners exist in the others area.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Intersects(AABB other)
        {
            return (
                Contains(new Point(other.Center.X - other.Half.X, other.Center.Y - other.Half.Y, 0)) ||
                Contains(new Point(other.Center.X + other.Half.X, other.Center.Y - other.Half.Y, 0)) ||
                Contains(new Point(other.Center.X + other.Half.X, other.Center.Y + other.Half.Y, 0)) ||
                Contains(new Point(other.Center.X - other.Half.X, other.Center.Y + other.Half.Y, 0)) ||
                other.Contains(new Point(Center.X - Half.X, Center.Y - Half.Y, 0)) ||
                other.Contains(new Point(Center.X + Half.X, Center.Y - Half.Y, 0)) ||
                other.Contains(new Point(Center.X + Half.X, Center.Y + Half.Y, 0)) ||
                other.Contains(new Point(Center.X - Half.X, Center.Y + Half.Y, 0))
            );
        }
    }
}
