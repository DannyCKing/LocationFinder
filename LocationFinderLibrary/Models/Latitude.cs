using LocationFinderLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationFinderLibrary.Models
{
    public class Latitude
    {
        public double LatitudeValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Allows you to construct a latitude with a decimal latitude.
        /// A negative value represents latitudes south of the equator.
        /// Ex: 40.7689, -21.444
        /// </summary>
        /// <param name="latitude">The decimal value of the latitude.</param>
        public Latitude(double latitude)
        {
            LatitudeValue = latitude;
        }

        /// <summary>
        /// Allows you to construct a latitude with the format of degrees, minutes, seconds and direction
        /// </summary>
        /// <param name="degrees">The degrees portion of the coordinate</param>
        /// <param name="minute">The minutes portion of the coordiate</param>
        /// <param name="seconds">The seconds portion of the coordinate</param>
        /// <param name="direction">The direction(North or South)</param>
        public Latitude(int degrees, int minutes, double seconds, Direction direction)
        {
            if (direction == Direction.East || direction == Direction.West)
            {
                throw new InvalidOperationException("Cannot create a latitude with a West or East direction");
            }

            LatitudeValue = DegreeUtilities.ConvertDegreeMinuteSecondsToDecimalDegrees(degrees, minutes, seconds, direction);
        }

        public static bool IsValidLatitude(string degreeString)
        {
            double degreeValue;
            if(double.TryParse(degreeString, out degreeValue))
            {
                return degreeValue >= -90 && degreeValue <= 90;
            }
            else
            {
                return false;
            }
        }
    }
}
