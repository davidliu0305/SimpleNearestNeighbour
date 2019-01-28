using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NearestNeighbour
{
    public class NearestPoint
    {
        /// <summary>
        /// the input data
        /// </summary>
        public List<Point> Samples { get; set; }

        /// <summary>
        /// position of the input
        /// </summary>
        public Point InputPoint { get; set; }


        public NearestPoint(List<Point> samples, Point inputPoint)
        {
            this.Samples = samples;
            this.InputPoint = inputPoint;
        }

        /// <summary>
        /// Search the nearest point of the input point 
        /// </summary>
        /// <returns></returns>
        public Point NearestPointSearch()
        {
            //min max x y for the buffer zone
            double xMax = Samples.Max(s => s.X);
            double yMax = Samples.Max(s => s.Y);
            double xMin = Samples.Min(s => s.X);
            double yMin = Samples.Min(s => s.Y);

            bool searchFinished = false;
            //depth of the quadTree
            int depth = 0;

            //RWDI points in the final search range
            List<Point> finalPoints = new List<Point>();

            //get the center and size of the buffer zone
            var center = new Point((xMin + xMax) / 2.0, (yMin + yMax) / 2.0, 0);
            var half = new Point((xMax - xMin) / 2.0, (yMax - yMin) / 2.0, 0);

            //the original quadtree
            QuadTree originalQt = new QuadTree(new AABB(center, half));
            while (!searchFinished)
            {
                //subdivide the quadtree
                QuadTree newQt = originalQt.SubdivideInput(InputPoint, depth, Samples);
                if (newQt != originalQt)
                {
                    depth++;
                    originalQt = newQt;
                }
                else
                {
                    searchFinished = true;
                    finalPoints = newQt.GetElements();
                    Point nearestGrid = FinalSearchChoice(InputPoint, finalPoints);
                    return nearestGrid;
                }
            }

            return null;
        }

        /// <summary>
        /// get the closest RWDI Grid for the potential planting area
        /// </summary>
        /// <param name="InputPoint"></param>
        /// <param name="finalPoints"></param>
        /// <returns></returns>
        private static Point FinalSearchChoice(Point InputPoint, List<Point> finalPoints)
        {
            double dist = double.MaxValue;
            Point nearestGrid = new Point();
            foreach (Point fp in finalPoints)
            {
                var newDist = Math.Pow((fp.X - InputPoint.X), 2) + Math.Pow((fp.Y - InputPoint.Y), 2);
                if (newDist <= dist)
                {
                    dist = newDist;
                    nearestGrid = fp;
                }
            }
            return nearestGrid;
        }
    }
}
