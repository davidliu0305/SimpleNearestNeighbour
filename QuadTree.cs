using System;
using System.Collections.Generic;
using System.Text;

namespace NearestNeighbour
{
    public class QuadTree
    {
        /// <summary>
        /// Maximum number of values per AABB zone.
        /// </summary>
        private const int QT_NODE_CAPACITY = 30;
        private List<Point> _elements = new List<Point>();

        private AABB _bounds;
        private int _count;
        private QuadTree _northWest;
        private QuadTree _northEast;
        private QuadTree _southWest;
        private QuadTree _southEast;

        public QuadTree(AABB bounds)
        {
            _bounds = bounds;
            _count = 0;
        }

        public void Dispose()
        {
            // if one corner exists they all exist
            // clean up all corners
            if (_northWest != null)
            {
                _northWest = null;
                _northEast = null;
                _southWest = null;
                _southEast = null;
            }
        }

        /// <summary>
        /// Number of data in this quadtree.
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return _count;
        }

        public List<Point> GetElements()
        {
            return this._elements;
        }

        /// <summary>
        /// sub divide the RWDI input
        /// </summary>
        /// <param name="farmPoint"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public QuadTree SubdivideInput(Point farmPoint, int depth, List<Point> RWDIInput)
        {
            // determine quarter size
            Point half = _bounds.Half;
            //first time search, give the RWDIPoints as input
            if (depth == 0)
            {
                this._elements = RWDIInput;
            }
            half.X /= 2.0;
            half.Y /= 2.0;

            // prepare each quadrant as a new quadtree
            _northWest = new QuadTree(new AABB(new Point(_bounds.Center.X - half.X, _bounds.Center.Y + half.Y, 0), half));
            _northWest._elements = _northWest.QueryRangeInput(_northWest._bounds, this._elements);

            if (_northWest._bounds.Contains(farmPoint) && _northWest._elements.Count > 0)
            {
                return _northWest;
            }
            _northEast = new QuadTree(new AABB(new Point(_bounds.Center.X + half.X, _bounds.Center.Y + half.Y, 0), half));
            _northEast._elements = _northEast.QueryRangeInput(_northEast._bounds, this._elements);
            if (_northEast._bounds.Contains(farmPoint) && _northEast._elements.Count > 0)
            {
                return _northEast;
            }
            _southWest = new QuadTree(new AABB(new Point(_bounds.Center.X - half.X, _bounds.Center.Y - half.Y, 0), half));
            _southWest._elements = _southWest.QueryRangeInput(_southWest._bounds, this._elements);
            if (_southWest._bounds.Contains(farmPoint) && _southWest._elements.Count > 0)
            {
                return _southWest;
            }
            _southEast = new QuadTree(new AABB(new Point(_bounds.Center.X + half.X, _bounds.Center.Y - half.Y, 0), half));
            _southEast._elements = _southEast.QueryRangeInput(_southEast._bounds, this._elements);
            if (_southEast._bounds.Contains(farmPoint) && _southEast._elements.Count > 0)
            {
                return _southEast;
            }

            half.X *= 2.0;
            half.Y *= 2.0;
            return this;
        }

        /// <summary>
        /// Query all the points in this range
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public List<Point> QueryRangeInput(AABB range, List<Point> elements)
        {
            // points list
            List<Point> points = new List<Point>();

            // simple case, get all points stored in this quad that are within the range
            foreach (Point point in elements)
            {
                if (range.Contains(point))
                {
                    points.Add(point);
                }
            }
            return points;
        }

    } 
}
