using System;
using System.Collections.Generic;
using System.Text;

namespace NearestNeighbour
{
    public class Point
    {
        /// <summary>
        /// X Coordinate
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y Coordinate
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// The value of the point.
        /// </summary>
        public double Value { get; set; }


        /// <summary>
        /// Comma seperated list of values for this point.
        /// </summary>
        public string Values { get; set; }

        /// <summary>
        /// Get coordinates
        /// </summary>
        public double[] Coordinates
        {
            get
            {
                double[] coord = { this.X, this.Y };

                return coord;
            }

        }
        /// <summary>
        /// The almighty Point constructor.
        /// </summary>
        public Point() { }

        /// <summary>
        /// Point with no value.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Point with a value.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public Point(double x, double y, double value)
        {
            X = x;
            Y = y;
            Value = value;
        }

        /// <summary>
        /// Constructs a point using a string value instead of a double
        /// and attempts to parse it. Sets 0 as value if unsuccessful.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="coordinates"></param>
        public Point(double x, double y, string value)
        {
            // Out value
            double parsedValue = 0;
            // Attempt to parse the value
            double.TryParse(value, out parsedValue);
            // Set values
            Value = parsedValue;
            X = x;
            Y = y;
        }

        public double Distance(Point s)
        {
            return Math.Sqrt((X - s.X) * (X - s.X) + (Y - s.Y) * (Y - s.Y));
        }
    }
}
